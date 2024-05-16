<?php
if (isset($_POST['register'])) {
    $mysqli = new mysqli("localhost", "root", "", "stive");

    if ($mysqli->connect_error) {
        die("Connection failed: " . $mysqli->connect_error);
    }

    $stmt = $mysqli->prepare("INSERT INTO users (nom, prenom, mail, mdp) VALUES (?, ?, ?, ?)");
    $stmt->bind_param("ssss", $nom, $prenom, $mail, $mdp);

    $nom = $_POST['nom'];
    $prenom = $_POST['prenom'];
    $email = $_POST['mail'];
    $mdp = $_POST['mdp'];
    $mdp = password_hash($mdp, PASSWORD_DEFAULT);

    if ($stmt->execute()) {
        echo "CA MARCHE!";
    } else {
        echo "Erreur : " . $stmt->error;
    }

    $stmt->close();
    $mysqli->close();
}
?>



<form action="register.php" method="post">
  <label for="nom">Nom:</label> 
  <input id="nom" name="nom" required="" type="text" />
<label for="prenom">Prenom:</label> 
  <input id="prenom" name="prenom" required="" type="text" />
  <label for="mail">Mail:</label>
  <input id="mail" name="mail" required="" type="email" />
  <label for="mdp">Mdp:</label>
  <input id="mdp" name="mdp" required="" type="password" />
  <input name="register" type="submit" value="Register" />
</form>
