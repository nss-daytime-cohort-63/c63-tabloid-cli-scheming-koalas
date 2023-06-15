SELECT * FROM Author

SELECT * FROM  Blog

SELECT * FROM Journal

SELECT * FROM Note

SELECT * FROM Post

SELECT * FROM Tag

SELECT * FROM Post 
JOIN PostTag ON Post.Id = PostTag.PostId
JOIN Tag ON PostTag.TagId = Tag.Id