<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';

class UtilisateurController {
    
    public $att_utilisateur = ['id', 'nom', 'prenom','mdp', 'mail', 'is_client', 'is_admin'];

    function get($id){
        if ($id) {

            $utilisateur = Utilisateur::find_one($id);
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
                foreach ($this->att_utilisateur as $att) {
                    $utilisateurArray[$att] = $p->$att;
                }
                $tableau[] = $utilisateurArray;
            }
            echo json_encode($tableau);
        }
    }


    function post($data, $headers){

        if($headers['action-type'] === 'register') {
            $utilisateur = Utilisateur::create();
            foreach ($this->att_utilisateur as $att) {
                if ($att !== 'id'){
                    if ($att === 'mdp'){
                        $mdp = 
                        $utilisateur->$att = password_hash($data->$att, PASSWORD_DEFAULT);
                    } else {
                        $utilisateur->$att = $data->$att;
                    }

                }
            }
            $utilisateur->save();
            $tab['id'] = $utilisateur->id;
            echo json_encode($tab);

        }  else if ($headers['action-type'] === 'login') {

            if (isset($data->mail) && isset($data->mdp)) {
                $mail = $data->mail;
                $password = $data->mdp;
                $utilisateur = Utilisateur::where('mail', $mail)->find_one();
                if ($utilisateur) {
                    if (password_verify($password, $utilisateur->mdp)) {
                        echo 'connected';
                    }
                } else {
                    echo "Pas d'utilisateur trouvé avec cet email.";
                }
                
            } else {
                echo 'Pas de data envoyée';
            }

        } else {
            echo 'Header non-authorisé';
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
