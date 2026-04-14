#language: pt-br
Funcionalidade: Editar Usuário

Contexto:
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário cadastrado com o ID 2 e e-mail "original@fiap.com"

Cenário: Editar usuário com sucesso
	Quando eu solicitar a edição do usuário 2 com os dados:
		| Nome          | Email         | PerfilId |
		| Nome Alterado | novo@fiap.com |        1 |
	Então o sistema deve retornar o status 200 OK

Esquema do Cenário: Erro ao tentar editar usuário inserindo dados inválidos
	Quando eu solicitar a edição do usuário 2 trocando o "<campo>" para "<valor>"
	Então o sistema deve retornar o status 400 BadRequest

Exemplos:
	| campo | valor          |
	| Email | email_invalido |
	| Senha | sfraca         |

Cenário: Erro ao tentar editar usuário atribuindo e-mail duplicado
	Dado que existe outro usuário cadastrado com o e-mail "existente@fiap.com"
	Quando eu solicitar a edição do usuário 2 trocando seu e-mail para "existente@fiap.com"
	Então o sistema deve retornar o status 409 Conflict

Cenário: Erro ao tentar editar um usuário que não existe
	Quando eu solicitar a edição do usuário 999
	Então o sistema deve retornar o status 404 NotFound