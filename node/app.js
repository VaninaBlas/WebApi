console.log("hola mundd");

let port = process.env.PORT || 3000;

let express = require('express');
let app = express();

app.use(express.json());

let usuarios = [
  {
    IdUsuario: 1,
    Nombre: "Vanina",
    Email: "vanyabrilconblas@gmail.com",
    NombreUsuario: "VaninaBlas",
    Contraseña: "chilipicante",
    Habilitado: true,
    FechaCreacion: "2024-09-22",
  },
  {
    IdUsuario: 2,
    Nombre: "Priscila",
    Email: "pri@gmail.com",
    NombreUsuario: "Pri",
    Contraseña: "ex",
    Habilitado: true,
    FechaCreacion: "2024-09-16",
  },
  {
    IdUsuario: 3,
    Nombre: "Jasmin",
    Email: "jasmin@gmail.com",
    NombreUsuario: "Jas",
    Contraseña: "veinticinco,veintiuno",
    Habilitado: true,
    FechaCreacion: "2024-05-12",
  },
];
let roles = [
  {
    IdRol: 1,
    Nombre: "Preceptor",
    Habilitado: true,
    FechaCreacion: "2024-09-20",
  },
  { IdRol: 2, Nombre: "Alumno", Habilitado: true, FechaCreacion: "2024-07-20" },
  {
    IdRol: 3,
    Nombre: "Profesor",
    Habilitado: true,
    FechaCreacion: "2024-08-20",
  },
];

// endpoints
app.post("/usuario", (request, response) => {
  let usuario = request.body;
  if (
    usuario.Nombre != null &&
    usuario.Nombre != "" &&
    usuario.Email != null &&
    usuario.Email != "" &&
    usuario.NombreUsuario != null &&
    usuario.NombreUsuario != "" &&
    usuario.Contraseña != null &&
    usuario.Contraseña != ""
  ) {
    usuarios.push(usuario);
    //response.send(usuarios);
    response.status(201).json(usuarios);
  } else {
    response.status(404); //bad request
  }
});
app.get("/usuarios", (req, res) => {
  res.send(usuarios);
});
app.get("/usuario/", (req, res) => {
  let usuarioEspecifico = usuarios.find(
    (u) => u.IdUsuario === parseInt(request.query.IdUsuario)
  ); // con metodos
  if (usuarioEspecifico != null) {
    res.status(200).json(usuarioEspecifico);
  } else {
    res.status(400);
  }
});
app.listen(port, () => {
  console.log(`Servidor corriendo en el puerto ${port}`);
});
