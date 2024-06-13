<?php
header('Content-Type: application/json; charset=utf-8');
require_once __DIR__ . '/controllers/ProduitController.php';
require_once __DIR__ . '/controllers/CommandeController.php';
require_once __DIR__ . '/controllers/UtilisateurController.php';


$url = isset($_SERVER['REDIRECT_URL']) ? $_SERVER['REDIRECT_URL'] : '';
$method = $_SERVER['REQUEST_METHOD'];

$headers = getallheaders();

$image = '';
$data = '';

if (isset($_FILES['image'])){
    $image = $_FILES['image'];
}

if (!empty(file_get_contents('php://input'))){
    $data = json_decode(file_get_contents('php://input'));
} else if (isset($_POST['data'])) {
    $data = json_decode($_POST['data']);
}

$base_url = "/STIVE/api/";
$page = str_replace($base_url, '', $url);
$url_list = explode('/', $page);
$controller = '';

if (isset($url_list[1]) && is_numeric($url_list[1])) {
    $id = $url_list[1];
} else {
    $id = '';
}

// Page redirect
switch ($url_list[0]) {
    case '':
        header("Location: acceuilapi.php");
        break;
    case 'produit':
        $controller = new ProduitController();
        break;
    case 'utilisateur':
        $controller = new UtilisateurController();
        break;
    case 'commande':
        $controller = new CommandeController();
        break;
    case 'test':
        echo 'test';
        break;
    // add case for more page
    default:
        http_response_code(404);
        echo json_encode(["error" => "Page non trouvée"]);
    }

// Method redirect
if ($controller) {
    switch ($method) {
        case 'GET':
            $controller->get($id, $_GET, $data);
            break;
        case 'POST':
            $controller->post($data, $headers, $image);
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
}
?>