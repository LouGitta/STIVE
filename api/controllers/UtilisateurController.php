<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';
require_once realpath(__DIR__ . '/../vendor/autoload.php');

use Firebase\JWT\JWT;
// Récupère la clé secrète
$secretKey = parse_ini_file(__DIR__ . '/../../jwtHandler.env')['JWT_SECRET_KEY'] ?? null;

// Classe qui regroupe les actions des Utilisateurs
class UtilisateurController {

    // Attributs de la table utilisateur
    public $att_utilisateur_full = ['id', 'nom', 'prenom','mdp', 'mail', 'is_client', 'is_admin'];
    public $att_utilisateur_register = ['id', 'nom', 'prenom','mdp', 'mail'];
    public $secretKey;

    public function __construct() {
        global $secretKey;
        $this->secretKey = $secretKey;
    }

    // Actions de la méthode GET uniquement pour les administrateurs
    function get($param, $authorization){
        if ($authorization == 'admin'){
            // S'il y a un id récupère l'utilisateur correspondant
            if (isset($param['id'])) {
                $utilisateur = Utilisateur::find_one($param['id']);
                if ($utilisateur) {
                    echo json_encode($utilisateur->as_array());
                } else {
                    http_response_code(404);
                    echo json_encode(["error" => "Utilisateur non trouve"]);
                }
            // Sinon récupère tous les utilisateurs
            } else {
                $utilisateurs = Utilisateur::find_many();
                $tableau = [];
                foreach ($utilisateurs as $p) {
                    $utilisateurArray = [];
                    foreach ($this->att_utilisateur_full as $att) {
                        $utilisateurArray[$att] = $p->$att;
                    }
                    $tableau[] = $utilisateurArray;
                }
                echo json_encode($tableau);
            }
        } else {
                http_response_code(401);
                echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
    }

    // Actions de la méthode POST
    function post($data, $headers){
        $att_utilisateur = [];
        // Si la requête vient du site 
        if ($headers['app-type'] === 'website'){
           $att_utilisateur = $this->att_utilisateur_register;
        } else {
           $att_utilisateur = $this->att_utilisateur_full;
        }
        // Actions de l'inscription
        if($headers['action-type'] === 'register') {
            $utilisateur = Utilisateur::create();
            foreach ($att_utilisateur as $att) {
                if ($att !== 'id'){
                    if ($att === 'mdp'){

                    $utilisateur->$att = password_hash($data->$att, PASSWORD_DEFAULT);
                } else {
                        $utilisateur->$att = $data->$att;
                    }

                }
            }
            $utilisateur->save();
            $tab['id'] = $utilisateur->id;
            $response['success'] = $utilisateur->id;
            echo json_encode($response);
        // Actions de connexion
        }  else if ($headers['action-type'] === 'login') {

            if (isset($data->mail) && isset($data->mdp)) {
                $mail = $data->mail;
                $password = $data->mdp;
                $utilisateur = Utilisateur::where('mail', $mail)->find_one();
                if ($utilisateur) {
                    if (password_verify($password, $utilisateur->mdp)) {
                        // Réponse pour l'application
                        if ($headers['app-type'] === 'adminapp' && $utilisateur->is_admin != 1){
                            http_response_code(401);
                            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
                        } else {
                        
                            $payload = [
                                'iat' => time(),
                                'exp' => time() + 86400,
                                'data' => [
                                    'id' => $utilisateur->id,
                                    'prenom' => $utilisateur->prenom,
                                    'admin' => $utilisateur->is_admin,
                                    'client' => $utilisateur->is_client
                                ]
                            ];
                            
                            $jwt = JWT::encode($payload, $this->secretKey, 'HS256');
                            $connected = array("id" => $utilisateur->id, "name" => $utilisateur->nom, "admin" => $utilisateur->is_admin);

                            echo json_encode(['status' => 'success','token' => $jwt]);

                        } 
                    }
                } else {
                    echo json_encode(['status' => 'error', 'message' => "Pas d'utilisateur trouvé avec cet email."]);
                }
                
            } else {
                echo json_encode(['status' => 'error', 'message' => "Pas de data envoyée"]);
            }

        } else {
            echo json_encode(['status' => 'error', 'message' => "Header non-authorisé"]);
        }

    }
    // Action pour la méthode Patch uniquement admin
    function patch($id, $data){
        if ($authorization == 'admin'){
            if ($id){
                $utilisateur = Utilisateur::find_one($id);
                foreach ($data as $key => $value){
                $utilisateur->$key = $value;
                }
                $utilisateur->save();
                $tab['id'] = $utilisateur->id;
                echo json_encode($tab);

            } else {
            http_response_code(405);
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
            }
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
    }
    // Action de la methode DELETE
    function delete($id){
        if ($id){
            $utilisateur = Utilisateur::find_one($id);
            if (!$utilisateur){
                http_response_code(404);
                echo json_encode(['status' => 'error', 'message' => "Aucun utilisateur trouvé"]);
            } else {
                $utilisateur->delete();
                echo json_encode(['status' => 'success', 'message' => "L'utilisateur à bien été supprimé"]);

            }
        } else {
            http_response_code(405);
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
        }
    }
        
}
