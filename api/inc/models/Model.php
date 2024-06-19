<?php
// Lien d'une class vers une table de la bdd
class Produit extends Model{

    public static $_table = "produit";

}

class Utilisateur extends Model{

    public static $_table = "utilisateur";

}

class Commande extends Model{

    public static $_table = "commande";

}

class Article extends Model{

    public static $_table = "article";

}

class Restock extends Model{

    public static $_table = "restock";

}
