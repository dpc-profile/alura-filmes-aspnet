A versão do dotnet é 6.0

# Banco de dados

Ele se comunica com um mysql, que é criado via docker composer, que está na pasta db-docker
```
cd db-docker
```
```
docker compose up -d
```
Se precisar parar os container, volte até a pasta __db-docker__ e execute o comando
```
docker compose down
```


Com o banco de dados rodando, na raiz do projeto execute o comando para criar as tabelas

```
dotnet ef database update
```

# O projeto
Para executar o projeto
```
dotnet watch
```

Enquanto executando, é possivel acessar a documentação da API no endereço
```
https://localhost:7298/swagger
```

Alguns exemplos de request estão no arquivo __api-test.http__