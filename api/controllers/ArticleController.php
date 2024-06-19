<?php
header('Content-Type: application/json; charset=utf-8');

require_once __DIR__ . '/../inc/config.inc.php';
require_once __DIR__ . '/../inc/models/Model.php';


class ArticleController {
    public $att_article = ['commande_id', 'produit_id', 'prix','quantite'];
        
    
    // Actions liées à la méthode GET
    function get($param){
        // Si paramètre commandes renvoie les articles liés
        if ($param['commande']) {
            $tableau = [];
                $articles = Article::where('commande_id',$param['commande'])->find_many();
                foreach ($articles as $p) {
                    $articleArray = [];
                    foreach ($this->att_article as $att) {
                        $articleArray[$att] = $p->$att;
                    }
                    $tableau[] = $articleArray;
                }
            echo json_encode($tableau);

        // Si paramètre id renvoie l'article avec l'id correspondant
        } else if ($param['id']) {
            $article = Article::find_one($param['id']);
            if ($article) {
                echo json_encode($article->as_array());
            } else {
                http_response_code(404);
                echo json_encode(["error" => "Article non trouve"]);
            }
        } else {
            echo json_encode(['status' => 'error', 'message' => "Parametre non-reconnu"]);
        }
    }
}
