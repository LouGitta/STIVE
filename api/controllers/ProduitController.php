<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';

// Classe qui regroupe les actions des produits
class ProduitController {
    // Liste des attributs de la table produit
    public $att_produit = ['id', 'nom', 'prix','description', 'annee', 'quantite_stock', 'reference', 'fournisseur', 'info', 'maison', 'famille', 'region', 'image'];

    // Actions liées à la méthode GET
    function get($param){
        // S'il y a un id ne récupère que le produit correspondant
        if (isset($param['id'])) {

            $produit = Produit::find_one($param['id']);
            if ($produit) {
                echo json_encode($produit->as_array());
            } else {
                http_response_code(404);
                echo json_encode(['status' => 'error', 'message' => "¨Produit non trouvé"]);
            }
        // Sinon récupère tous les produits 
        }  else {
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

    // Actions liées à la méthode POST disponible que pour les administrateurs
    function post($data, $param, $image, $authorization){
        if ($authorization == 'admin'){
            $produit = Produit::create();
            foreach ($this->att_produit as $att) {
                if ($att !== 'id'){
                    if ($att === 'image') {
                        $produit->$att = "";
                        // basename($image['name']);
                    } else {
                        $produit->$att = $data->$att;
                    }
                }
            }
            $produit->save();
            $tab['id'] = $produit->id;
                        
            // if (isset($image)) {
            //     $image_name = basename($image['name']);
            //     $dossier_cible = __DIR__ . "/../../front/imageProduit/";
            //     $lieu_image = $dossier_cible . $image_name;
    
            //     $fileType = pathinfo($lieu_image, PATHINFO_EXTENSION);
            //     $check = getimagesize($image['tmp_name']);
    
            //     if ($check !== false) {
            //         $allowedTypes = array('jpg', 'png', 'jpeg', 'gif');
            //         if (in_array($fileType, $allowedTypes)) {
            //             if (move_uploaded_file($image['tmp_name'], $lieu_image)) {
            //                 echo json_encode(['status' => 'success', 'message' => "L'image à bien été téléchargée"]);
            //             } else {
            //                 echo json_encode(['status' => 'error', 'message' => "Une erreur s'est produite pendant le téléchargement de l'image"]);
            //             }
            //         } else {
            //             echo json_encode(['status' => 'error', 'message' => "Seulement les formats JPG, JPEG, PNG, & GIF sont autorisés"]);

            //         }
            //     } else {
            //         echo json_encode(['status' => 'error', 'message' => "Le fichier n'est pas une image"]);
            //     }
            // } else {
            //     echo json_encode(['status' => 'error', 'message' => "Aucun fichier uploadé"]);
            // }
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
    }
    
    // Actions liées à la méthode PATCH disponible uniquement pour les administrateurs
    function patch($id, $data, $authorization){
        if ($authorization == 'admin'){
            
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
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
            }
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
    }

    // Actions liées à la méthode DELETE
    function delete($id){
        if ($id){
            $produit = Produit::find_one($id);
            if (!$produit){
                http_response_code(404);
                echo json_encode(['status' => 'error', 'message' => "¨Produit non trouvé"]);
            } else {
                $produit->delete();
                echo json_encode(['status' => 'success', 'message' => "Le produit à bien été supprimé"]);
            }
        } else {
            http_response_code(405);
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
        }
    }
        
}
