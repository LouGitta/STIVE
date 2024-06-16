<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';
require_once realpath(__DIR__ . '/../vendor/autoload.php');

use Firebase\JWT\JWT;
$secretKey = parse_ini_file(__DIR__ . '/../../jwtHandler.env')['JWT_SECRET_KEY'] ?? null;



class UtilisateurController {
    
    public $att_utilisateur_full = ['id', 'nom', 'prenom','mdp', 'mail', 'is_client', 'is_admin'];
    public $att_utilisateur_register = ['id', 'nom', 'prenom','mdp', 'mail'];
    public $secretKey;

    public function __construct() {
        global $secretKey;
        $this->secretKey = $secretKey;
    }

    function get($param){
        if (isset($param['id'])) {

            $utilisateur = Utilisateur::find_one($param['id']);
            if ($utilisateur) {
                echo json_encode($utilisateur->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Utilisateur non trouve"]);
            }
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
    }


    function post($data, $headers){
        $att_utilisateur = [];

        if ($headers['app-type'] === 'website'){
           $att_utilisateur = $this->att_utilisateur_register;
        } else {
           $att_utilisateur = $this->att_utilisateur_full;
        }

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

        }  else if ($headers['action-type'] === 'login') {

            if (isset($data->mail) && isset($data->mdp)) {
                $mail = $data->mail;
                $password = $data->mdp;
                $utilisateur = Utilisateur::where('mail', $mail)->find_one();
                if ($utilisateur) {
                    if (password_verify($password, $utilisateur->mdp)) {

                        $payload = [
                            'iat' => time(),
                            'exp' => time() + 36000,
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

    function patch($id, $data){
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
        echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }

    function delete($id){
        if ($id){
            $utilisateur = Utilisateur::find_one($id);
            if (!$utilisateur){
                http_response_code(406);
                echo json_encode(["error" => "Aucun utilisateur correspondant"]);
            } else {
                $utilisateur->delete();
                $tab["message"] = "Le utilisateur à bien été supprimé";
                echo json_encode($tab);
            }
        } else {
            http_response_code(405);
            echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }
        
}
