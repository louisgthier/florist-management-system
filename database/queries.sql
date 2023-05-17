
-- Get all items in an arrangement
SELECT * FROM florist.item
JOIN florist.arrangement_contains ON florist.item.name = florist.arrangement_contains.item_name
WHERE arrangement_id = 1;
