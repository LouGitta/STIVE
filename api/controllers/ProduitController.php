<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';

class ProduitController {
    
    public $att_produit = ['id', 'nom', 'prix','description', 'annee', 'quantite_stock', 'reference', 'fournisseur', 'info', 'maison', 'famille', 'region', 'image'];

    function get($id){
        if ($id) {

            $produit = Produit::find_one($id);
            if ($produit) {
                echo json_encode($produit->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Produit non trouve"]);
            }

        } else {
            $produits = Produit::find_many();
            $tableau = [];
            foreach ($produits as $p) {
                $produitArray = [];
                foreach ($this->att_produit as $att) {
                    $produitArray[$att] = $p->$att;
                }
                $tableau[] = $produitArray;
            }
            echo json_encode($tableau);
        }
    }


    function post($data){
        $produit = Produit::create();
        foreach ($this->att_produit as $att) {
            if ($att !== 'id'){
                $produit->$att = $data->$att;
            }
        }
        $produit->save();
        $tab['id'] = $produit->id;
        echo json_encode($tab);

    }

    function patch($id, $data){
        if ($id){
            $produit = Produit::find_one($id);
            foreach ($data as $key => $value){
            $produit->$key = $value;
            }
            $produit->save();
            $tab['id'] = $produit->id;
            echo json_encode($tab);

        } else {
        http_response_code(405);
        echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }

    function delete($id){
        if ($id){
            $produit = Produit::find_one($id);
            if (!$produit){
                http_response_code(406);
                echo json_encode(["error" => "Aucun produit correspondant"]);
            } else {
                $produit->delete();
                $tab["message"] = "Le produit à bien été supprimé";
                echo json_encode($tab);
            }
        } else {
            http_response_code(405);
            echo json_encode(["error" => "Pas d'id fourni"]);
        }
    }
        
}
