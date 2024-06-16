<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';


class RestockController {

    public $att_restock = ['id','reference_commande', 'produit_id', 'date', 'quantite', 'prix'];

    function get($param){
        if (isset($param['id'])) {
            
            $restock = Restock::find_one($param['id']);
            if ($restock) {
                echo json_encode($restock->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Restock non trouve"]);
            }
           
        } else {

            $restocks = Restock::find_many();
            $tableau = [];
            foreach ($restocks as $r) {
                $restockArray = [];
                foreach ($this->att_restock as $att) {
                    $restockArray[$att] = $r->$att;
                }
                $tableau[] = $restockArray;
            }
            echo json_encode($tableau);
        }
    }

    function restock($data){
        $reference = date('Y-m-d') .'/'. substr(hash('sha256', date('Y-m-d H:i:s:ms')), 8, 8);

            $restock = Restock::create();
            foreach ($this->att_restock as $att) {
                if ($att !== 'id'){
                    if ($att === 'reference_commande') {
                        $restock->$att = $reference;
                    } else if ($att === 'produit_id'){
                        $restock->$att = $data->produit_id;
                    } else if ($att === 'quantite'){
                        $restock->$att = "150";
                    } else if ($att === 'date'){
                        $restock->$att = date('Y-m-d');
                    } else {
                        $restock->$att = $data->$att;
                    }
                }
            }
            $restock->save();
            $this->stockUpdate($data);
    }

    function stockUpdate($data){
        $produit = Produit::find_one($data->produit_id);
        // print_r($produit->quantite_stock);
        $produit->quantite_stock += 150;
        // print_r($produit->quantite_stock);

        $produit->save();
        // echo json_encode(['status' => 'success', 'message' => "Restock valide"]);

    }
}