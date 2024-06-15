<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';
require_once __DIR__ . '/RestockController.php';

class CommandeController {
    
    public $att_commande = ['id', 'reference_commande', 'date_commande', 'utilisateur_id', 'quantite_commande', 'prix_commande'];
    public $att_article = ['id','commande_id', 'produit_id', 'prix', 'quantite' ];

    function get($param){
        if (isset($param['id'])) {

            $commande = Commande::find_one($param['id']);
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
        $reference = $data->date_commande .'/'. substr(hash('sha256', date('Y-m-d H:i:s:ms')), 8, 8);
        try {
            // Creation de la commande
            $commande = Commande::create();
                foreach ($this->att_commande as $att) {
                    if ($att !== 'id'){
                        if ($att === 'reference_commande') {
                            $commande->$att = $reference;
                        } else if ($att === 'utilisateur_id') {
                            '';
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
            $succes = array('status' => 'Validé', 'message' => 'La commande est validée', 'numero' => $reference);
            echo json_encode($succes, JSON_UNESCAPED_UNICODE);
            
            // Mise a jour du stock des produits
            $this->stockUpdate($data);

        } catch (Exception $e) {
            $error = array('error' => $e, 'message' => 'La commande a échoué');
            echo json_encode($error, JSON_UNESCAPED_UNICODE);
        }

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
        
    // Modification du stock
    function stockUpdate($data){
            $produit = Produit::find_one($data->produit_id);
            print_r($produit->quantite_stock);
            $produit->quantite_stock -= $data->quantite;
            print_r($produit->quantite_stock);
            $produit->save();

            echo json_encode(['status' => 'success', 'message' => "Modification du stock validee"]);

            if ($produit->quantite_stock <= 30){
                echo json_encode(['status' => 'warning', 'message' => "Restock en cours"]);
                $restock = new RestockController();
                $restock->restock($data);
            }
    }
}