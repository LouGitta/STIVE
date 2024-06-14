<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';


class ProduitController {
    
    public $att_produit = ['id', 'nom', 'prix','description', 'annee', 'quantite_stock', 'reference', 'fournisseur', 'info', 'maison', 'famille', 'region', 'image'];

    function get($param){
        if (isset($param['id'])) {

            $produit = Produit::find_one($param['id']);
            if ($produit) {
                echo json_encode($produit->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Produit non trouve"]);
            }
        } elseif ($param) {
            $tableau = [];
            foreach($param as $key => $value){
                $produits = Produit::where($key,$value)->find_many();
                foreach ($produits as $p) {
                    $produitArray = [];
                    foreach ($this->att_produit as $att) {
                        $produitArray[$att] = $p->$att;
                    }
                    $tableau[] = $produitArray;
                }
            }
            $unique_tableau = [];
            foreach ($tableau as $element) {
                $unique_tableau[$element['id']] = $element;
            }
            $produits = array_values($unique_tableau);
            echo json_encode($produits);


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


    function post($data, $param, $image){
        
        $produit = Produit::create();
        foreach ($this->att_produit as $att) {
            if ($att !== 'id'){
                if ($att === 'image') {
                    $produit->$att = basename($image['name']);
                } else {
                    $produit->$att = $data->$att;
                }
            }
        }
        $produit->save();
        $tab['id'] = $produit->id;
                    
        if (isset($image)) {
            $image_name = basename($image['name']);
            $dossier_cible = __DIR__ . "/../../front/imageProduit/";
            $lieu_image = $dossier_cible . $image_name;

            $fileType = pathinfo($lieu_image, PATHINFO_EXTENSION);
            $check = getimagesize($image['tmp_name']);

            if ($check !== false) {
                $allowedTypes = array('jpg', 'png', 'jpeg', 'gif');
                if (in_array($fileType, $allowedTypes)) {
                    if (move_uploaded_file($image['tmp_name'], $lieu_image)) {
                        $response['file'] = "The file has been uploaded successfully.";
                    } else {
                        $response['file'] = "Sorry, there was an error uploading your file.";
                    }
                } else {
                    $response['file'] = "Sorry, only JPG, JPEG, PNG, & GIF files are allowed.";
                }
            } else {
                $response['file'] = "File is not an image.";
            }
        } else {
                $response['file'] = "No file was uploaded.";
            }

        print_r($response);
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