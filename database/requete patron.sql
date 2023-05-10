SELECT AVG(standard_bouquet.price)
FROM standard_bouquet JOIN purchase_order 
ON purchase_order.bouquet_name = standard_bouquet.name;

SELECT count(purchase_order.id),client.id,client.first_name,client.name
FROM client JOIN 
(SELECT * FROM purchase_order WHERE)
ON client.id=purchase_order.client_id 
GROUP BY client.client_id
ORDER BY count(purchase_order.id);

