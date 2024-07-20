<h1> Orientações para subir o projeto: </h1>

<p> Na raiz do projeto rodar o comando: </p>

```
docker compose up --build
```

<p> Após subir o projeto, acessar a url: </p>

```
http://localhost:8080/swagger/index.html
```

<p> Certifique-se de que o prometheus esteja com os serviços UP: </p>

```
http://localhost:9090/targets
```

<p> Para acessar o grafana, acesse a url: </p>

```
http://localhost:3000
usuario: admin
senha: @admin
```

<p> Faça algumas requisições na API para alimentar o grafana. </p>