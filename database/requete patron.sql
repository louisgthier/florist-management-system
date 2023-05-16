-- prix moyen bouquet standard
SELECT AVG(standard_bouquet.price)
FROM standard_bouquet JOIN purchase_order 
ON purchase_order.bouquet_name = standard_bouquet.name;

-- prix moyen arrangement floral
SELECT AVG(flower_arrangement.price)
FROM flower_arrangement JOIN purchase_order
ON flower_arrangement.id=purchase_order.arrangement_id;

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
FROM shop JOIN standard_bouquet