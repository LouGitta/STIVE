<?php
header('Content-Type: application/json; charset=utf-8');
require_once __DIR__ . '/controllers/ProduitController.php';
require_once __DIR__ . '/controllers/CommandeController.php';
require_once __DIR__ . '/controllers/UtilisateurController.php';
require_once __DIR__ . '/controllers/ArticleController.php';

use Firebase\JWT\JWT;
use Firebase\JWT\Key;

$url = isset($_SERVER['REDIRECT_URL']) ? $_SERVER['REDIRECT_URL'] : '';
// Récupère la méthode de la requête
$method = $_SERVER['REQUEST_METHOD'];

// Récupère les headers de la requête
$headers = getallheaders();

$image = '';
$data = '';

// Récupère l'image de la requête
if (isset($_FILES['image'])){
    $image = $_FILES['image'];
}

// Récupère les données fournies dans la requêtes
if (!empty(file_get_contents('php://input'))){
    $data = json_decode(file_get_contents('php://input'));
} else if (isset($_POST['data'])) {
    $data = json_decode($_POST['data']);
}
// Emplacement de l'api après le domaine
$base_url = "/STIVE/api/";

// Ne garde que l'endpoint de la requête
$page = str_replace($base_url, '', $url);
$url_list = explode('/', $page);
$controller = '';

// Demande les authorisations de l'utilisateurs
$authorization = authorize($headers);

// Redire vers la page de l'api correspondantes
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

// Redirige vers la methode correspondante GET = récupère, POST = Ajout, PATCH = Mise à jour, DELETE = suppression
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

// Regarde les autorisations de l'utilisateurs
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
