INSERT INTO client (nom, prenom, email, mot_de_passe, num_tel, addresse, num_cb, date_inscription)
VALUES
('Martin', 'Sophie', 'sophie.martin@gmail.com', 'motdepasse0', 0123456789, '10 Rue du Bac, Paris', 1234567890123456, '2017-03-14'),
('Durand', 'Luc', 'luc.durand@yahoo.fr', 'motdepasse1', 0234567890, '28 Rue des Champs, Lyon', 2345678901234567, '2018-08-02'),
('Petit', 'Charlotte', 'charlotte.petit@hotmail.com', 'motdepasse2', 0345678901, '17 Rue de la Paix, Marseille', 3456789012345678, '2016-05-21'),
('Lefebvre', 'Maxime', 'maxime.lefebvre@gmail.com', 'motdepasse3', 0456789012, '11 Rue de la Gare, Lille', 4567890123456789, '2021-02-10'),
('Moreau', 'Emma', 'emma.moreau@gmail.com', 'motdepasse4', 0567890123, '9 Rue de la Liberté, Bordeaux', 5678901234567890, '2015-11-30'),
('Girard', 'Hugo', 'hugo.girard@yahoo.fr', 'motdepasse5', 0678901234, '13 Rue des Fleurs, Toulouse', 6789012345678901, '2019-10-15'),
('Robin', 'Léa', 'lea.robin@hotmail.com', 'motdepasse6', 0789012345, '21 Rue des Lilas, Nice', 7890123456789012, '2017-07-07'),
('Rousseau', 'Ethan', 'ethan.rousseau@gmail.com', 'motdepasse7', 0890123456, '6 Rue du Marché, Nantes', 8901234567890123, '2022-01-05'),
('Jean', 'Inès', 'ines.jean@yahoo.fr', 'motdepasse8', 0901234567, '2 Rue des Écoles, Strasbourg', 9012345678901234, '2016-09-18'),
('Dupont', 'Léo', 'leo.dupont@hotmail.com', 'motdepasse9', 0912345678, '12 Rue de la Poste, Grenoble', 0123456789012345, '2018-06-12'),
('Benoit', 'Amandine', 'amandine.benoit@gmail.com', 'motdepasse10', 0123456789, '14 Rue du Palais, Montpellier', 1234567890123456, '2015-04-29'),
('Dubois', 'Antoine', 'antoine.dubois@yahoo.fr', 'motdepasse11', 0234567890, '19 Rue de la Mairie, Rennes', 2345678901234567, '2020-11-22');

INSERT INTO bouquet_standard (nom_bouquet, description, prix, categorie) 
VALUES 
('Gros Merci', 'Arrangement floral avec marguerites et verdure', 45, 'Toute occasion'),
('L’amoureux', 'Arrangement floral avec roses blanches et roses rouges', 65, 'St-Valentin'),
('L’Exotique', 'Arrangement floral avec ginger oiseaux du paradis roses et genet', 40, 'Toute occasion'),
('Maman', 'Arrangement floral avec gerbera roses blanches lys et alstroméria', 80, 'Fête des mères'),
('Vive la mariée', 'Arrangement floral avec lys et orchidées', 120, 'Mariage'),
('Le Printanier', 'Arrangement floral avec tulipes et jonquilles', 35, 'Toute occasion'),
('Belle Epoque', 'Arrangement floral avec pivoines et roses', 90, 'Anniversaire'),
('Le Jardin Anglais', 'Arrangement floral avec roses anglaises et lavande', 55, 'Toute occasion'),
('Fleurs du Sud', 'Arrangement floral avec lavande et tournesols', 50, 'Toute occasion'),
('Le Sophistiqué', 'Arrangement floral avec calla et roses', 75, 'Mariage');

-- Fleurs
INSERT INTO produit (nom_produit, prix_produit, type_produit, disponibilite) VALUES 
('Gerbera', 5.00, 'f', 'à lannée'),
('Ginger', 4.00, 'f', 'à lannée'),
('Glaïeul', 1.00, 'f', 'mai à novembre'),
('Marguerite', 2.25, 'f', 'à lannée'),
('Rose rouge', 2.50, 'f', 'à lannée'),
('Tulipe', 3.50, 'f', 'février à mai'),
('Orchidée', 12.00, 'f', 'à lannée'),
('Lys', 7.50, 'f', 'à l''année'),
('Bouquet de pivoines', 20.00, 'f', 'mai à juin'),
('Chrysanthème', 1.50, 'f', 'septembre à novembre'),
('Oeillet', 1.75, 'f', 'à lannée'),
('Lavande', 4.50, 'f', 'juin à septembre'),
('Anthurium', 8.00, 'f', 'à lannée'),
('Dahlia', 3.00, 'f', 'juillet à septembre'),
('Edelweiss', 6.00, 'f', 'juin à août'),
('Œillet d''Inde', 2.00, 'f', 'juin à octobre'),
('Freesia', 4.50, 'f', 'mars à juin'),
('Iris', 5.50, 'f', 'mars à juin'),
('Lilas', 6.50, 'f', 'avril à juin'),
('Pensée', 2.50, 'f', 'à lannée');

-- Items
INSERT INTO produit (nom_produit, prix_produit, type_produit, disponibilite) VALUES 
('Pot en terre cuite', 6.00, 'i', 'à lannée'),
('Vase en verre', 8.00, 'i', 'à lannée'),
('Cache-pot en osier', 10.00, 'i', 'à lannée'),
('Panier en rotin', 12.00, 'i', 'à lannée'),
('Tuteur pour plante', 4.50, 'i', 'à lannée'),
('Arrosoir en métal', 15.00, 'i', 'à lannée'),
('Pince à élaguer', 7.50, 'i', 'à lannée'),
('Griffe à main', 3.50, 'i', 'à lannée'),
('Gants de jardinage', 6.00, 'i', 'à lannée'),
('Sécateur de jardin', 10.00, 'i', 'à lannée');


INSERT INTO arrangement_floral (prix_donne) VALUES (15);
INSERT INTO arrangement_floral (prix_donne) VALUES (60);
INSERT INTO arrangement_floral (prix_donne) VALUES (80);
INSERT INTO arrangement_floral (prix_donne) VALUES (25);
INSERT INTO arrangement_floral (prix_donne) VALUES (65);
INSERT INTO arrangement_floral (prix_donne) VALUES (90);
INSERT INTO arrangement_floral (prix_donne) VALUES (40);
INSERT INTO arrangement_floral (prix_donne) VALUES (20);
INSERT INTO arrangement_floral (prix_donne) VALUES (75);
INSERT INTO arrangement_floral (prix_donne) VALUES (12);
INSERT INTO arrangement_floral (prix_donne) VALUES (30);
INSERT INTO arrangement_floral (prix_donne) VALUES (70);
INSERT INTO arrangement_floral (prix_donne) VALUES (50);
INSERT INTO arrangement_floral (prix_donne) VALUES (85);
INSERT INTO arrangement_floral (prix_donne) VALUES (95);
INSERT INTO arrangement_floral (prix_donne) VALUES (18);
INSERT INTO arrangement_floral (prix_donne) VALUES (45);
INSERT INTO arrangement_floral (prix_donne) VALUES (80);
INSERT INTO arrangement_floral (prix_donne) VALUES (60);
INSERT INTO arrangement_floral (prix_donne) VALUES (35);

INSERT INTO commande (adresse_livraison, message, date_livraison, date_commande, etat_commande, id_client, num_bouquet, nom_bouquet)
VALUES
('10 Rue du Bac, Paris', 'Merci pour tout', '2015-12-01 12:00:00', '2015-11-01 12:00:00', 'CL', 1, NULL, 'Gros Merci'),
('28 Rue des Champs, Lyon', 'Joyeux anniversaire', '2015-12-05 12:00:00', '2015-11-05 12:00:00', 'CL', 2, NULL, 'Le Printanier'),
('17 Rue de la Paix, Marseille', 'Félicitations', '2015-12-09 12:00:00', '2015-11-09 12:00:00', 'CL', 3, NULL, 'Fleurs du Sud'),
('11 Rue de la Gare, Lille', 'Joyeuses fêtes', '2015-12-13 12:00:00', '2015-11-13 12:00:00', 'CL', 4, NULL, 'Belle Epoque'),
('9 Rue de la Liberté, Bordeaux', 'Bon rétablissement', '2015-12-17 12:00:00', '2015-11-17 12:00:00', 'CL', 5, NULL, 'L’Exotique'),
('13 Rue des Fleurs, Toulouse', 'Félicitations pour le bébé', '2015-12-21 12:00:00', '2015-11-21 12:00:00', 'CL', 6, NULL, 'Maman'),
('21 Rue des Lilas, Nice', 'Je t’aime', '2015-12-25 12:00:00', '2015-11-25 12:00:00', 'CL', 7, NULL, 'L’amoureux'),
('6 Rue du Marché, Nantes', 'Bon courage', '2015-12-29 12:00:00', '2015-11-29 12:00:00', 'CL', 8, NULL, 'Le Jardin Anglais'),
('2 Rue des Écoles, Strasbourg', 'Félicitations pour le diplôme', '2016-01-02 12:00:00', '2015-12-02 12:00:00', 'CL', 9, NULL, 'Fleurs du Sud'),
('12 Rue de la Poste, Grenoble', 'Joyeux Noël', '2016-01-06 12:00:00', '2015-12-06 12:00:00', 'CL', 10, NULL, 'Le Jardin Anglais'),
('14 Rue du Palais, Montpellier', 'Bonne fête maman', '2016-01-10 12:00:00', '2015-12-10 12:00:00', 'CL', 11, NULL, 'L’Exotique'),
('12 Rue du Moulin', 'Joyeux anniversaire', '2015-06-15 10:00:00', '2015-05-15 10:00:00', 'CL', 1, NULL, 'Gros Merci'),
('6 Rue des Fleurs', 'Félicitations !', '2015-07-01 12:00:00', '2015-06-01 12:00:00', 'CL', 5, NULL, 'Vive la mariée'),
('19 Rue de la Mairie', 'Bonne fête maman', '2015-05-24 14:00:00', '2015-04-24 14:00:00', 'CL', 4, NULL, 'Maman'),
('14 Rue du Palais', 'Pour toi mon amour', '2015-06-15 16:00:00', '2015-05-15 16:00:00', 'CL', 10, NULL, 'L’Exotique'),
('11 Rue de la Gare', 'Bonne fête papa', '2015-06-22 10:00:00', '2015-05-22 10:00:00', 'CL', 1, NULL, 'Gros Merci'),
('21 Rue des Lilas', 'Bon rétablissement', '2015-07-03 10:00:00', '2015-06-03 10:00:00', 'CL', 8, NULL, 'Le Jardin Anglais');
