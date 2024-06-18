// Global function
function getUser() {
    if (!window.localStorage.getItem("token")) {
        return null;
    } else {
        const token = localStorage.getItem('token')
        const arrayToken = token.split('.');
        const tokenPayload = JSON.parse(atob(arrayToken[1]));
        return tokenPayload.data;
    }
}

function displayNoUserElements() {
    const noUserElements = document.querySelectorAll(".noUserElement");
    for (let element of noUserElements) {
        element.classList.toggle("hidden");
    }
}

function displayUserElements() {
    const userElements = document.querySelectorAll(".userElement");
    for (let element of userElements) {
        element.classList.toggle("hidden");
    }

    const user = getUser();
    const userNameElement = document.querySelector(".user--container span");
    userNameElement.innerHTML = user.prenom;
}

function showCard(card) {
    if (card.classList.contains("hidden")) {
        card.classList.toggle("hidden");
    }
}

function hideCard(card) {
    if (!card.classList.contains("hidden")) {
        card.classList.toggle("hidden");
    }
}


function disconnect(){
    window.localStorage.removeItem("token")
    location.reload()
}

// Accueil function
async function getProduits() {
    // url pour récupérer ma liste d'articles //
    const url = "/STIVE/api/produit";

    try {
        // récupération des données depuis l'api //
        const response = await fetch(url);
        const data = await response.json();
        console.table(data);

        return data;
    } catch (error) {
        // si erreur, on affiche le message //
        console.log(error.message);
    }
}
// si erreur afficher un message //
function afficherNoProduitsElements() {
    const message = "Il n'y a pas d'article à présenter !";
    const errorMessageElement = document.querySelector(
        ".errorMessageElement"
    );

    errorMessageElement.innerHTML = message;
    errorMessageElement.classList.toggle("hidden");
}

function afficherProduitsElements(products) {
    // l'élément qui reçoit les cartes //
    const container = document.querySelector(".cards--container");

    for (let product of products) {
        // le template pour créer les cartes plus simplement //
        const template = document
            .querySelector("#templateCard")
            .content.cloneNode(true);

        // href => redirection vers page détail du produit //
        template.querySelector("a").href = `produit.html?=${product.id}`;
        // dataset pour faciliter le travail avec les filtres //
        template.querySelector("a").dataset.famille = product.famille;
        if (product.image) {
            template.querySelector("img").src = `/STIVE/front/imageProduit/${product.image}`
        } else {
            console.log('no image')
        }
        // afficher les informations dans les cartes //
        template.querySelector(".nom").innerHTML = product.nom;
        template.querySelector(".region").innerHTML = product.region;
        template.querySelector(".famille").innerHTML = product.famille;

        // ne pas oublier d'insérer les cartes dans leur container //
        container.appendChild(template);
    }
}

function initFilters() {
    const filters = document.querySelectorAll(".filter");

    for (let filter of filters) {
        filter.addEventListener("click", handleFilterClick);
    }
}

// @clik sur un filtre //
function handleFilterClick(event) {
    const filterType = event.currentTarget.value;

    const cards = document.querySelectorAll(".card--container");
    for (let card of cards) {
        if (card.dataset["famille"] === filterType) {
            showCard(card);
        } else {
            hideCard(card);
        }
    }
}


// Produit function
async function getProduit() {
    const id = window.location.search.split('=')[1]
    // url pour récupérer mon produit //
    const url = "/STIVE/api/produit/?id="+id;

    try {
        // récupération des données depuis l'api //
        const response = await fetch(url);
        const data = await response.json();
        console.table(data);

        return data;
    } catch (error) {
        // si erreur, on affiche le message //
        console.log(error.message);
    }
}

function afficherProduitElements(product) {
    // l'élément qui reçoit les cartes //
    document.querySelector(".produit-page").textContent = product.nom;
    const container = document.querySelector(".produit-container");

    // le template pour créer les cartes plus simplement //
    const template = document
    .querySelector("#templateDetailProduit")
    .content.cloneNode(true);
    
    // afficher les informations dans les cartes //
    if (product.image) {
        img = template.querySelector("img")
        img.src = `/STIVE/front/imageProduit/${product.image}`;
        img.width = 300;
        img.height = 300;
    } else {
        console.log('no image')
    }

    template.querySelector("#nom").textContent = product.nom;
    template.querySelector("#prix").textContent = `${product.prix}`+' €';
    template.querySelector("#description").textContent = product.description;
    template.querySelector("#quantite").max = product.quantite_stock <= 36 ? product.quantite_stock : 36;
    template.querySelector(".help-block").innerHTML = product.quantite_stock <= 36 ? `Limité à ${product.quantite_stock} par commande`: 'Limité à 36 par commande';


    // ne pas oublier d'insérer les cartes dans leur container //
    container.appendChild(template);
    localStorage.setItem('produitActuel', JSON.stringify(product));
}

function afficherNoProduitElements() {
    const message = "Cet article n'existe pas";
    const errorMessageElement = document.querySelector(
        ".errorMessageElement"
    );

    errorMessageElement.innerHTML = message;
    errorMessageElement.classList.toggle("hidden");
}


function addToCart(){
    const quantite = document.getElementById("quantite").value
    const produit = JSON.parse(localStorage.getItem('produitActuel'))

    const panier = {
    'quantite': quantite,
    'produit': produit
    };

    let storage = localStorage.getItem('panier');

    if (storage) {
        const panierExistant = JSON.parse(storage);
        for (const item of panierExistant){
            if (item.produit.id === produit.id){
                const cumul = parseInt(item.quantite) + parseInt(quantite);

                if (cumul > item.produit.quantite_stock || cumul > 36) {
                    item.quantite = item.produit.quantite_stock <= 36 ? item.produit.quantite_stock : 36;
                    alert(`Hop Hop Hop laissez en un peu pour les autres\nLa quantité max a été ajouté au panier (${item.quantite})`)
                } else if (cumul <= item.produit.quantite_stock || cumul <= 36) {
                    item.quantite = cumul;
                } else {
                    console.log(cumul)
                }
            } else {
                panierExistant.push(panier);
            }

        }
                            
        localStorage.setItem('panier', JSON.stringify(panierExistant));

    } else {

        const quantite = parseInt(panier.quantite)
        if (quantite > produit.quantite_stock || quantite > 36) {
            panier.quantite = produit.quantite_stock <= 36 ? produit.quantite_stock : 36;
            localStorage.setItem('panier', JSON.stringify([panier]));
            alert(`NON MAIS OH ON AVAIT MIS UNE LIMITE! Hop c'est BAN\nLa quantité max a été ajouté au panier (${panier.quantite})`)

        } else if (quantite <= produit.quantite_stock || quantite <= 36) {
            localStorage.setItem('panier', JSON.stringify([panier]));
        } else {
            console.log(quantite)
        }
    }
    
    alert("L'article à bien été ajouté au panier.")
    location.reload()
}

// Commande function
async function getCommandes() {
    // url pour récupérer ma liste d'articles //
    const user_id = getUser();
    if (user_id){
        const url = `/STIVE/api/commande/?utilisateur_id=${user_id.id}`;

        try {
            // récupération des données depuis l'api //
            const response = await fetch(url);
            const data = await response.json();

            return data;
        } catch (error) {
            // si erreur, on affiche le message //
            console.log(error.message);
        }
    } else {
        alert("Vous n'avez rien à faire là !")
        window.location.href ="/STIVE/front/login";
    }
}

// si erreur afficher un message //
function afficherNoCommandesElements() {
    const message = "Aucunes commandes passées !";
    const errorMessageElement = document.querySelector(
        ".errorMessageElement"
    );

    errorMessageElement.innerHTML = message;
    errorMessageElement.classList.toggle("hidden");
}

// affichage des produits //
function afficherCommandesElements(commandes) {
    // l'élément qui reçoit les cartes //
    const container = document.querySelector(".cards--container--commande");

    for (let commande of commandes) {
        // le template pour créer les cartes plus simplement //
        const template = document
            .querySelector("#templateCard")
            .content.cloneNode(true);

        // afficher les informations dans les cartes //
        template.querySelector(".reference_commande").innerHTML = commande.reference_commande;
        template.querySelector(".quantite_commande").innerHTML = commande.quantite_commande;
        template.querySelector(".prix_commande").innerHTML = commande.prix_commande + ' €';

        // ne pas oublier d'insérer les cartes dans leur container //
        container.appendChild(template);
    }
}


// Inscription Connexion function
async function inscription(event) {
    event.preventDefault();

    const form = event.target.closest('form');
    
    const formData = new FormData(form);
    
    const formValues = Object.fromEntries(formData.entries());
    if (formValues.nom != "" && formValues.prenom != "" && formValues.mail != "" && formValues.mdp != "" && formValues.mdp-confirmer != ""){
        if (formValues.mdp-confirmer === formValues.mdp) {

            const response = await fetch("/STIVE/api/utilisateur/", {
                method: "POST",
                body: JSON.stringify(formValues),
                headers: {
                    "Content-type": "application/json; charset=UTF-8",
                    "action-type": "register",
                    "app-type": "website"
                }
            })
            
                const data = await response.json();

            if (data.success) {
                alert("Inscription réussie");
                window.location.replace("/STIVE/front/login.html")
            } else {
                console.log("Registration failed:", data.message);
            }
        } else {
            alert("Le mot de passe doit être identique")
        }
    } else {
        alert("Au moins un champ n'est pas rempli")
    }
}    



async function connexion(event){
    event.preventDefault();
    
    const form = event.target.closest('form');
    
    const formData = new FormData(form);
    
    const formValues = Object.fromEntries(formData.entries());
    
    if (formValues.mail != "" && formValues.mdp != ""){
        const response = await fetch("/STIVE/api/utilisateur/", {
            method: "POST",
            body: JSON.stringify(formValues),
            headers: {
            "Content-type": "application/json; charset=UTF-8",
            "action-type": "login",
            "app-type": "website"
            }
        })

        const data = await response.json();
        if (data.status) {
            window.localStorage.setItem("token", data.token)

            alert("Connexion réussie");
            window.location.replace("/STIVE/front/index.html")
        } else {
            console.log("Connexion échouée:", data.message);
        }
    } else {
        alert("Mail ou mot de passe manquant")
    }
}

function afficherMdp() {
    const passwordInput = document.getElementById('mdp');
    const passwordIcon = document.getElementById('afficherMdp');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordIcon.classList.remove('fa-eye');
        passwordIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        passwordIcon.classList.remove('fa-eye-slash');
        passwordIcon.classList.add('fa-eye');
    }
}

function afficherMdpConfirmer() {
    const passwordInput = document.getElementById('mdp-confirmer');
    const passwordIcon = document.getElementById('afficherMdpConfirmer');
    if (passwordInput.type === 'password') {
        passwordInput.type = 'text';
        passwordIcon.classList.remove('fa-eye');
        passwordIcon.classList.add('fa-eye-slash');
    } else {
        passwordInput.type = 'password';
        passwordIcon.classList.remove('fa-eye-slash');
        passwordIcon.classList.add('fa-eye');
    }
}

// Panier function
async function getProduitsPanier() {
    const produits = JSON.parse(localStorage.getItem('panier'))
    
    if (produits) {
        console.table(produits)
        return produits
    }
}

function afficherPanierElements(panier) {
    // l'élément qui reçoit les cartes //
    // const confirmerPanierElement = document.querySelector(".confirmer--panier");
    // if (confirmerPanierElement) {
    //     confirmerPanierElement.classList.toggle("hidden");
    // }

    const container = document.querySelector(".produit--panier");

    var prixTotal = 0;
    var quantiteTotal = 0;

    for (let produit of panier) {
        // le template pour créer les cartes plus simplement //
        const template = document
            .querySelector("#produitPanier")
            .content.cloneNode(true);

        // dataset pour faciliter le travail avec les filtres //
        const quantite = produit.quantite
        const prixUnite = produit.produit.prix;
        const totalProduit = quantite*prixUnite;

        quantiteTotal += parseInt(quantite);
        prixTotal += totalProduit;
        // afficher les informations dans les cartes //
        if (produit.produit.image) {
            img = template.querySelector("img")
            img.src = `/STIVE/front/imageProduit/${produit.produit.image}`;
            img.width = 150;
            img.height = 150;
        } else {
            console.log('no image')
        }
        template.querySelector(".nom").innerHTML = produit.produit.nom;
        template.querySelector(".region").innerHTML = produit.produit.region;
        template.querySelector(".famille").innerHTML = produit.produit.famille;
        template.querySelector(".quantite").innerHTML = quantite;
        template.querySelector(".prix--cumul").innerHTML = totalProduit +' €';
        template.querySelector(".prix--unite").innerHTML = produit.produit.prix+'€ / unité';
        // console.log(template)

        // ne pas oublier d'insérer les cartes dans leur container //
        container.appendChild(template);

        
    }
    document.querySelector(".quantite--total").innerHTML = quantiteTotal + ' produits';
    document.querySelector(".sous--total").innerHTML = prixTotal + ' €';
}

function afficherNoPanierElements() {
    const message = "Il n'y a pas d'article dans le panier !";
    const errorMessageElement = document.querySelector(
        ".errorMessageElement"
    );

    errorMessageElement.innerHTML = message;
    errorMessageElement.classList.toggle("hidden");
}

function validerPanier() {
    if (!localStorage.getItem('panier')){
        alert('Votre panier est vide VINGT DIEUX !')
    } else if (!localStorage.getItem('token')) {
        alert('Votre devez être connecté pour valider votre commande !')
    } else {
        document.querySelector(".livraison--emptybox").classList.toggle("hidden");
    }
}

function validerAdresse() {
    document.querySelector(".paiement").classList.toggle("hidden");
}

function getDate() {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Months are zero-based, so add 1
    const day = String(today.getDate()).padStart(2, '0');

    return `${year}-${month}-${day}`;
}

function validerPaiement() {
    alert('Paiement validé !');

    const token = localStorage.getItem('token')
    const arrayToken = token.split('.');
    const tokenPayload = JSON.parse(atob(arrayToken[1]));

    const data = JSON.parse(localStorage.getItem('panier'))

    var produits = [];

    var prixTotal = 0;
    var quantiteTotal = 0;

    for (let produit of data) {
        // dataset pour faciliter le travail avec les filtres //
        const id = produit.produit.id;
        const quantite = produit.quantite
        const prixUnite = produit.produit.prix;
        const totalProduit = quantite*prixUnite;

        quantiteTotal += parseInt(quantite);
        prixTotal += totalProduit;

        const panier = {
        'produit_id': id,
        'quantite': quantite,
        'prix' : prixUnite,
        };
        
        produits.push(panier)
    }
    
    const commande = {
        //'userid' : userid
        'date_commande' : getDate(),
        'quantite_commande': quantiteTotal,
        'utilisateur_id' : tokenPayload.data.id,
        'prix_commande': prixTotal,
        'produits' : produits,
    };
    const url = "/STIVE/api/commande";
    
    postCommande(url, commande)
}

async function postCommande(url, data) {
    const response = await fetch(url, {
        method: 'POST',
        headers: {
        'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    });

    const responseData = await response.json();
    console.log(responseData)
    if (responseData.status == 'success') {
        alert('Commande terminée.')
        localStorage.removeItem('panier')
        window.location.href ="/STIVE/front/commande";

    } else {
        console.log(responseData)
        alert('Problème dans la commande réassayez plus tard ou contactez le support.')
        window.location.reload()
    }

}