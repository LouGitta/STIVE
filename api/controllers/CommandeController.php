<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';
require_once __DIR__ . '/RestockController.php';

class CommandeController {
    // Attributs liés aux tables colonnes commande et article
    public $att_commande = ['id', 'reference_commande', 'date_commande', 'utilisateur_id', 'quantite_commande', 'prix_commande'];
    public $att_article = ['id','commande_id', 'produit_id', 'prix', 'quantite' ];

    // Actions liées à la méthode GET
    function get($param, $authorization){
        // Client autorisé à récupérer les commandes liées à son id
        if ($authorization == 'client'){
            if (isset($param['id'])) {
                $commande = Commande::find_one($param['id']);
                if ($commande) {
                    echo json_encode($commande->as_array());
                } else {
                    http_response_code(404);
                    echo json_encode(['status' => 'error', 'message' => "Aucune commande trouvé"]);
                }

            } else if (isset($param['utilisateur_id'])) {
                $commandes = Commande::where('utilisateur_id', $param['utilisateur_id'])->find_many();
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
        // Admin récupère toutes les commandes
        }else if ($authorization == 'admin'){
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
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
    }

    // Actions liées à la méthode POST
    function post($data, $headers, $image, $authorization){
        if ($authorization == 'admin' || 'client'){
            $reference = $data->date_commande .'/'. substr(hash('sha256', date('Y-m-d H:i:s:ms')), 8, 8);
            try {
                // Creation de la commande
                $commande = Commande::create();
                    foreach ($this->att_commande as $att) {
                        if ($att !== 'id'){
                            if ($att === 'reference_commande') {
                                $commande->$att = $reference;
                            } else {
                            $commande->$att = $data->$att;
                            }
                        }
                    }
                $commande->save();
                $tab['id'] = $commande->id;
                
                // Creation des articles de la commandes
                foreach ($data->produits as $data) {
                    $article = Article::create();
                        foreach ($this->att_article as $att) {
                            if ($att !== 'id'){
                                if ($att === 'commande_id') {
                                    $article->$att =  $tab['id'];
                                } else {
                                    $article->$att = $data->$att;
                                }
                            }
                        }
                    $article->save();
                }
                $succes = array('status' => 'success', 'message' => 'La commande est validée', 'numero' => $reference);
                echo json_encode($succes, JSON_UNESCAPED_UNICODE);
                
                // Mise a jour du stock des produits
                $this->stockUpdate($data);

            } catch (Exception $e) {
                $error = array('error' => $e, 'message' => 'La commande a échoué');
                echo json_encode($error, JSON_UNESCAPED_UNICODE);
            }
        }else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }

    }
    
    // Actions liées à la méthode PATCH
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
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
        }
    }

    // Actions liées à la méthode DELETE
    function delete($id){
        if ($id){
            $commande = Commande::find_one($id);
            if (!$commande){
                http_response_code(404);
                echo json_encode(['status' => 'error', 'message' => "Commande non trouvée"]);
            } else {
                $commande->delete();
                echo json_encode(['status' => 'success', 'message' => "La commande à bien été supprimé"]);

            }
        } else {
            http_response_code(405);
            echo json_encode(['status' => 'error', 'message' => "Pas d'id fourni"]);
        }
    }
        
    // Modification du stock
    function stockUpdate($data){
            $produit = Produit::find_one($data->produit_id);
            $produit->quantite_stock -= $data->quantite;
            $produit->save();

            // echo json_encode(['status' => 'success', 'message' => "Modification du stock validee"]);
            // Si le stock du produit est < à 36 il y a un restock automatique
            if ($produit->quantite_stock <= 36){
                // echo json_encode(['status' => 'warning', 'message' => "Restock en cours"]);
                $restock = new RestockController();
                $restock->restock($data);
            }
    }
}
