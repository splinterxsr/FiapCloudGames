#language: pt-br
Funcionalidade: Adicionar Usuário

Contexto:
    Dado que eu estou autenticado como "Administrador"

Cenário: Adicionar novo usuário com sucesso
    Dado que eu preencho os dados do novo usuário:
        | Nome          | Email            | Senha      | PerfilId |
        | Fulano de Tal | fulano@fiap.com  | Teste@123  |     1    |
    Quando eu solicitar a adição do usuário
    Então o sistema deve retornar o status 201 Created

    Cenário: Erro ao tentar adicionar usuário com e-mail já existente
    Dado que já existe um usuário cadastrado com o e-mail "duplicado@fiap.com"
    E que eu preencho os dados do novo usuário com o e-mail "duplicado@fiap.com"
    Quando eu solicitar a adição do usuário
    Então o sistema deve retornar o status 409 Conflict

Esquema do Cenário: Erro ao tentar adicionar usuário com dados inválidos
    Dado que eu tento adicionar um usuário com os seguintes dados:
        | Nome   | Email   | Senha   | PerfilId   |
        | <nome> | <email> | <senha> | <perfilId> |
    Quando eu solicitar a adição do usuário
    Então o sistema deve retornar o status 400 BadRequest
    E a resposta deve conter a mensagem de erro "<mensagem>"

    Exemplos:
        | nome  | email            | senha      | perfilId | mensagem                                                                                  |
        |       | teste@gmail.com  | Senha@123  | 1        | Insira o nome do usuário.                                                                 |
        | Luigi | email_invalido   | Senha@123  | 1        | O e-mail inserido é inválido.                                                             |
        | Peach | peach@gmail.com  | 123        | 1        | A senha precisa ter no mínimo 8 dígitos, contendo números, letras e caracteres especiais. |
        | Yoshi | yoshi@gmail.com  | Senha@123  | null     | Insira o ID do perfil do usuário.                                                         |