<?php
session_start();

if (isset($_POST['login'])) {
    $mysqli = new mysqli("localhost", "root", "", "stive");
    if ($mysqli->connect_error) {
        die("Connection failed: " . $mysqli->connect_error);
    }

    // Sanitize inputs
    $mail = mysqli_real_escape_string($mysqli, $_POST['mail']);
    $mdp = mysqli_real_escape_string($mysqli, $_POST['mdp']);

    $stmt = $mysqli->prepare("SELECT id, mdp FROM utilisateur WHERE mail = ?");
    $stmt->bind_param("s", $mail);

    $stmt->execute();
    $stmt->store_result();

    if ($stmt->num_rows > 0) {
        $stmt->bind_result($id, $hashed_password);
        $stmt->fetch();

        if (password_verify($mdp, $hashed_password)) {
            $_SESSION['loggedin'] = true;
            $_SESSION['id'] = $id;
            $_SESSION['mail'] = $mail;
            header("Location:http://localhost/STIVE/front/");
            exit;
        } else {
            echo "Mauvais mot de passe!";
        }
    } else {
        echo "Email non trouvé!";
    }

    $stmt->close();
    $mysqli->close();
}
?>

<form action="login.php" method="post">
    <label for="mail">Mail:</label>
    <input id="mail" name="mail" required="" type="text" />
    <label for="mdp">Mdp:</label>
    <input id="mdp" name="mdp" required="" type="password" />
    <input name="login" type="submit" value="Se connecter" />
    <br>
    Pas encore inscrit : <a href="http://localhost/STIVE/api/login_system/register.php">S'inscrire</a>
</form>
