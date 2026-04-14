#language: pt-br
Funcionalidade: Inativar Usuário

Contexto:
    Dado que eu estou autenticado como "Administrador"

Cenário: Inativar usuário com sucesso
    Dado que existe um usuário cadastrado com o ID 2
    Quando eu solicitar a inativação do usuário 2
    Então o sistema deve retornar o status 200 OK

Cenário: Erro ao tentar inativar usuário inexistente
    Dado que não existe um usuário com o ID 999
    Quando eu solicitar a inativação do usuário 999
    Então o sistema deve retornar o status 404 NotFound