<?php
header('Content-Type: application/json; charset=utf-8');
require_once __DIR__ . '/controllers/ProduitController.php';

$url = isset($_SERVER['REDIRECT_URL']) ? $_SERVER['REDIRECT_URL'] : '';
$method = $_SERVER['REQUEST_METHOD'];
$data = json_decode(file_get_contents('php://input'));

$base_url = "/STIVE/api/";
$page = str_replace($base_url, '', $url);
$url_list = explode('/', $page);

if (isset($url_list[1]) && is_numeric($url_list[1])) {
    $id = $url_list[1];
} else {
    $id = '';
}

switch ($url_list[0]) {
    case 'produit':
        $controller = new ProduitController();
        break;
    // add case for more page
    default:
        http_response_code(404);
        echo json_encode(["error" => "Page not found"]);
    }

switch ($method) {
    case 'GET':
        $controller->get($id);
        break;
    case 'POST':
        $controller->post($data);
        break;
    case 'PATCH':
        $controller->patch($id, $data);
        break;
    case 'DELETE':
        $controller->delete($id);
        break;
    default:
        http_response_code(405);
        
        echo json_encode(["error" => "Méthode non autorisée"]);
        break;
}

?>