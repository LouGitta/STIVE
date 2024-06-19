<?php
// Génère une page d'acceuil au format HTML si la requête vient d'un navigateur ou un JSON avec les pages principales.
$acceptHeader = $_SERVER['HTTP_ACCEPT'];

if (strpos($acceptHeader, 'text/html') !== false) {

    header('Content-Type: text/html');
    ?>
    <!DOCTYPE html>
    <html lang="fr">
    <head>
        <meta charset="UTF-8">
        <title>Page d'acceuil API</title>
    </head>
    <body>
        <h1>Bienvenue sur la page d'acceuil de l'API</h1>
        <ul>
            <li>Produits: <a href="http://localhost/STIVE/api/produit">http://localhost/STIVE/api/produit</a></li>
            <li>Utilisateurs: <a href="">http://localhost/STIVE/api/utilisateur</a></li>
            <li>Commandes: <a href="">http://localhost/STIVE/api/commande</a></li>
        </ul>
        
    </body>
    </html>
    <?php

} else {
    header('Content-Type: application/json');
    echo json_encode(['produit' => 'http://localhost/STIVE/api/produit', 'utilisateur' => 'http://localhost/STIVE/api/utilisateur', 'commande' => 'http://localhost/STIVE/api/commande',]);
}

?>
