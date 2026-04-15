#language: pt-br
Funcionalidade: Editar Usuário

Contexto:
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário cadastrado com ID 2

Cenário: Editar usuário com sucesso
	Dado que eu preencho os dados para editar o usuário 2:
		| Nome          | Email         | PerfilId |
		| Nome Alterado | novo@fiap.com |        1 |
	Quando eu solicitar a edição do usuário
	Então o sistema deve retornar o status 200 OK

Cenário: Erro ao tentar editar usuário com dados inválidos
	Dado que eu preencho os dados para editar o usuário 2:
		| Email   | Senha   |
		| <email> | <senha> |
	Quando eu solicitar a edição do usuário
	Então o sistema deve retornar o status 400 BadRequest
	E a resposta deve conter a mensagem de erro "<mensagem>"

Exemplos:
	| email          | senha | mensagem                                                                                  |
	| email_invalido |       | O e-mail inserido é inválido.                                                             |
	|                |   123 | A senha precisa ter no mínimo 8 dígitos, contendo números, letras e caracteres especiais. |

Cenário: Erro ao tentar editar usuário atribuindo e-mail duplicado
	Dado que já existe um usuário cadastrado com o e-mail "existente@fiap.com"
	E que eu preencho os dados para editar o usuário 2 com o e-mail "existente@fiap.com"
	Quando eu solicitar a edição do usuário
	Então o sistema deve retornar o status 409 Conflict

Cenário: Erro ao tentar editar um usuário que não existe
	Dado que não existe um usuário cadastrado com ID 999
	E que eu preencho os dados para editar o usuário 999:
		| Nome  | Email          | PerfilId |
		| Teste | teste@fiap.com |        1 |
	Quando eu solicitar a edição do usuário
	Então o sistema deve retornar o status 404 NotFound