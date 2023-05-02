# dockerMySql

## Rodando
Para executar o projeto e não travar o terminal, use o comando:
```sh
docker compose up -d
```

Para encerrar e remover os container, use:
```sh
docker compose down
```
___

## Usando o phpMyAdmin

Para acessar a pagina do phpMyAdmin
```sh
http://localhost:8081/
```

- Servidor = O nome do serviço lá no compose
___

## Sobre o volume

Ao rodar o compose, o volume sera criado com o seguinte nome:
```sh
<ProjectName>_<VolumeName>
```

___
## Portas
Porta padrão e exposta do mySql
```sh
3306:3306
```

Porta exposta do phpMyAdmin
```sh
8081
```

Porta padrão do phpMyAdmin
```sh
80
```
 