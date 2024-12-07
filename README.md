# ActionArc 🎬

## Welcome to the Todo Application - .NET Core Web API! 📞


**Link for Testing 🚀**: 

' | Scalar | Swagger
| :--- | ---: | :---:
Live <small>(Dev tunnel)</small> | <a href="https://t6105b8p-7108.inc1.devtunnels.ms/scalar/v1" target="_blank">Live</a> | <a href="https://t6105b8p-7108.inc1.devtunnels.ms/swagger" target="_blank">Live</a>
Localhost  | <small>`https://localhost:7108/scalar/v1`</small> | <small>`https://localhost:7108/swagger`</small>



Welcome to the *super exciting*, *edge-of-your-seat*, *world-changing* Todo Application built with .NET Core Web API! Okay, maybe it's not *that* dramatic, but it's a small and mighty app to manage your tasks with statuses like "Pending", "In Progress", and "Completed". Perfect for when you're learning to build APIs and need something to practice CRUD operations (or just want to track whether you’ve finished watching that 10-episode series...).

This app may not change the world, but it’ll definitely help you learn some important skills in .NET Core Web API while organizing your imaginary todo list. Keep your expectations realistic, and let's get to coding!

## Features

- **Create**: Add a new Todo. You can pretend to get things done while really just adding tasks to your list.
- **Read All**: View all your Todo items or filter for that one task you “totally did” but never really did.
- **Read by ID**: Want to check out a specific Todo? Retrieve a particular Todo by its ID! It's like opening a specific page of your to-do book, but faster and without the paper cuts.
- **Update**: Change the status of your Todos—because sometimes "In Progress" just feels more accurate than "Completed."
- **Delete**: Remove your failed tasks. It’s fine. We all have them.
- **Download CSV**: Want to export your Todo list to a CSV file? Now you can! Perfect for sharing with your friend or pretending to work when you're really just staring at a spreadsheet.


## API Endpoints

#### 1. **GET /todo/Get?id=1**
#### 2. **GET /todo/GetTodos?status=0&pageno=1&pagesize=1&&duedate=2024-02-02**
#### 3. **POST /todo/Create**
#### 4. **PUT /todo/Update**
#### 5. **DELETE /todo/Delete?id=1**
#### 6. **GET /todo/DownloadCSV**


### Additional Stuff

- CSV download feature
- Unit Test (XUnit Test)
- Deployed using Visual studio Dev tunnel
- Link for API (Scalar and Swagger)
- Global Exception handling
- In-Memory DB (Entity core)
- 6 Seeded Todos