-- prix moyen bouquet
SELECT AVG(standard_bouquet.price,flower_arrangement.price)
FROM standard_bouquet JOIN purchase_order JOIN flower_arrangement
ON purchase_order.bouquet_name = standard_bouquet.name AND flower_arrangement.id=purchase_order.arrangement_id;


-- client du mois
SELECT count(purchase_order.id),client.id,client.first_name,client.name
FROM client JOIN 
(SELECT * FROM purchase_order WHERE EXTRACT(MONTH FROM NOW())=EXTRACT(MONTH FROM purchase_order.order_date ))
ON client.id=purchase_order.client_id 
GROUP BY client.client_id
ORDER BY count(purchase_order.id)
LIMIT 1;

-- client de l'année
SELECT count(purchase_order.id),client.id,client.first_name,client.name
FROM client JOIN purchase_order
ON client.id=purchase_order.client_id 
WHERE purchase_order IN (SELECT * FROM purchase_order WHERE EXTRACT(YEAR FROM NOW())=EXTRACT(YEAR FROM purchase_order.order_date ))
GROUP BY client.id
ORDER BY count(purchase_order.id)
LIMIT 1;


