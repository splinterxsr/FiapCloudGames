#language: pt-br
Funcionalidade: Editar Jogo

Contexto:
    Dado que eu estou autenticado como "Administrador"

Cenário: Editar jogo com sucesso
    Dado que existe um jogo cadastrado com ID 1
    Quando eu solicitar a edição do jogo 1 com o nome "The Wolf Among Us"
    Então o sistema deve retornar o status 200 OK

Cenário: Erro ao tentar editar jogo inexistente
    Dado que não existe um jogo com ID 999
    Quando eu solicitar a edição do jogo 999 com o nome "Jogo Fantasma"
    Então o sistema deve retornar o status 404 NotFound