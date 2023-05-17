-- prix moyen bouquet standard
USE florist;
SELECT SUM(standard_bouquet.price),COUNT(standard_bouquet.price)
FROM standard_bouquet JOIN purchase_order 
ON purchase_order.bouquet_name = standard_bouquet.name;

-- prix moyen arrangement floral
SELECT SUM(flower_arrangement.price),COUNT(flower_arrangement.price)
FROM flower_arrangement JOIN purchase_order
ON flower_arrangement.id=purchase_order.arrangement_id;

-- prix moyen bouquet tout confondu
USE florist;
SELECT (SUM(standard_bouquet.price)+SUM(flower_arrangement.price))/(COUNT(flower_arrangement.price)+COUNT(standard_bouquet.price))
FROM standard_bouquet JOIN purchase_order JOIN flower_arrangement
ON purchase_order.bouquet_name = standard_bouquet.name OR flower_arrangement.id=purchase_order.arrangement_id;

SELECT AVG(price) AS PrixMoyen
FROM
(
   SELECT price FROM standard_bouquet JOIN purchase_order
   ON purchase_order.bouquet_name = standard_bouquet.name
   UNION ALL
   SELECT price FROM flower_arrangement
) AS Bouquets;

SELECT * FROM STANDARD_BOUQUET;

-- client du mois
SELECT COUNT(purchase_order.id), client.id, client.first_name, client.name
FROM client
JOIN purchase_order ON client.id = purchase_order.client_id
WHERE EXTRACT(MONTH FROM purchase_order.order_date) = EXTRACT(MONTH FROM NOW()) 
AND EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW()) 
GROUP BY client.id, client.first_name, client.name
ORDER BY COUNT(purchase_order.id) DESC
LIMIT 10;

-- client de l'année
SELECT COUNT(purchase_order.id), client.id, client.first_name, client.name
FROM client
JOIN purchase_order ON client.id = purchase_order.client_id
WHERE EXTRACT(YEAR FROM purchase_order.order_date) = EXTRACT(YEAR FROM NOW())
GROUP BY client.id, client.first_name, client.name
ORDER BY COUNT(purchase_order.id) DESC
LIMIT 10;

-- meilleur bouquet standard
SELECT COUNT(purchase_order.id),standard_bouquet.name
FROM standard_bouquet
JOIN purchase_order ON standard_bouquet.name = purchase_order.bouquet_name
GROUP BY standard_bouquet.name
ORDER BY COUNT(purchase_order.id) DESC
LIMIT 10; 

-- magasin ayant généré le plus gros chiffres d'affaire
SELECT shop.address,SUM(standard_bouquet.price)
FROM shop JOIN standard_bouquet JOIN purchase_order
ON shop.id=purchase_order.shop_id AND purchase_order.bouquet_name=standard_bouquet.name
GROUP BY shop.address
ORDER BY SUM(standard_bouquet.price) DESC
LIMIT 10;

-- fleur exotique la moins vendue
SELECT item_name, sum(quantity)
FROM arrangement_contains JOIN item
ON arrangement_contains.item_name=item.name
WHERE item.type='f'
GROUP BY item_name
ORDER BY SUM(quantity)
LIMIT 10;

