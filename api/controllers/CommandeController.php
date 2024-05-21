<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';

class CommandeController {
    
    public $att_commande = ['id', 'date_commande', 'utilisateur_id','produit_id', 'quantite_commande', 'prix_commande'];

    function get($id){
        if ($id) {

            $commande = Commande::find_one($id);
            if ($commande) {
                echo json_encode($commande->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Commande non trouve"]);
            }

        } else {
            $commandes = Commande::find_many();
            $tableau = [];
            foreach ($commandes as $p) {
                $commandeArray = [];
                foreach ($this->att_commande as $att) {
                    $commandeArray[$att] = $p->$att;
                }
                $tableau[] = $commandeArray;
            }
            echo json_encode($tableau);
        }
    }


    function post($data){
        $commande = Commande::create();
        foreach ($this->att_commande as $att) {
            if ($att !== 'id'){
                $commande->$att = $data->$att;
            }
        }
        $commande->save();
        $tab['id'] = $commande->id;
        echo json_encode($tab);

    }

    function patch($id, $data){
        if ($id){
            $commande = Commande::find_one($id);
            foreach ($data as $key => $value){
            $commande->$key = $value;
            }
            $commande->save();
            $tab['id'] = $commande->id;
            echo json_encode($tab);

        } else {
        http_response_code(405);
        echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }

    function delete($id){
        if ($id){
            $commande = Commande::find_one($id);
            if (!$commande){
                http_response_code(406);
                echo json_encode(["error" => "Aucun commande correspondant"]);
            } else {
                $commande->delete();
                $tab["message"] = "Le commande à bien été supprimé";
                echo json_encode($tab);
            }
        } else {
            http_response_code(405);
            echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }
        
}
