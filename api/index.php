<?php
header('Content-Type: application/json; charset=utf-8');
require_once __DIR__ . '/controllers/ProduitController.php';
require_once __DIR__ . '/controllers/CommandeController.php';
require_once __DIR__ . '/controllers/UtilisateurController.php';
require_once __DIR__ . '/controllers/ArticleController.php';

use Firebase\JWT\JWT;
use Firebase\JWT\Key;

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

$authorization = authorize($headers);

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
        if ($authorization == 'admin' || 'client'){
            $controller = new CommandeController();
        }  else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
        break;

    case 'article':
        if ($authorization == 'admin' || 'client'){
        $controller = new ArticleController();
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
        break;
        
    case 'restock':
        if ($authorization == 'admin'){
            $controller = new RestockController();
        } else {
            http_response_code(401);
            echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
        }
        break;
        // add case for more page
    default:
        http_response_code(404);
        echo json_encode(['status' => 'error', 'message' => "Page non trouvée"]);    
    }

        // Method redirect
        if ($controller) {
    switch ($method) {
        case 'GET':
            $controller->get($_GET, $authorization);
            break;

        case 'POST':
            $controller->post($data, $headers, $image, $authorization);
            break;

        case 'PATCH':
            if ($authorization == 'admin' || 'client'){
                $controller->patch($_GET['id'], $data, $authorization);
            } else {
                http_response_code(401);
                echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
            }
            break;
            
        case 'DELETE':
            if ($authorization == 'admin'){
                $controller->delete($_GET['id']);
            } else {
                http_response_code(401);
                echo json_encode(['status' => 'error', 'message' => "Vous n'avez pas les autorisations requises"]);
            }
            break;
        default:
            http_response_code(405);
            echo json_encode(['status' => 'error', 'message' => "Méthode non autorisée"]);
            break;
    }
}

function authorize($token){
    if (!empty($token['Authorization'])) {
        if (preg_match('/Bearer\s(\S+)/', $token['Authorization'], $matches)) {
            $secretKey = parse_ini_file(__DIR__ . '/../jwtHandler.env')['JWT_SECRET_KEY'] ?? null;
            $decoded = JWT::decode($matches[1], new Key($secretKey, 'HS256'));
            if ($decoded->data->admin === 1){
                return 'admin';
            } else {
                return 'client';
            }
        }
    } else {
        return 'rien';
    }
}

?>