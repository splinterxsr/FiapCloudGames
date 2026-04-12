#language: pt-br
Funcionalidade: Gestão de Usuários
  Como uma API com diferentes níveis de acesso
  Quero que o acesso às operações relacionadas aos usuários sejam controladas por perfil
  Para garantir que dados sensíveis sejam gerenciados apenas por administradores

# --- Regras - Adicionar Usuários ---
Cenário: Adicionar novo usuário com sucesso
	Dado que eu envio os dados válidos de um novo usuário
	Quando eu solicitar o cadastro do usuário
	Então o sistema deve retornar o status 201 Created
	E o usuário deve estar salvo no banco de dados

Cenário: Erro ao adicionar usuário com dados inválidos
	Dado que eu envio os dados de um novo usuário com <campo> inválido: "<valor>"
	Quando eu solicitar o cadastro
	Então o sistema deve retornar o status 400 BadRequest
Exemplos:
	| campo  | valor          |
	| e-mail | email_invalido |
	| senha  | sfraca         |
	| e-mail |                | # Dado faltante

Cenário: Erro ao adicionar usuário com e-mail duplicado
	Dado que já existe um usuário cadastrado com o e-mail "duplicado@fiap.com"
	Quando eu tentar cadastrar um novo usuário com o e-mail "duplicado@fiap.com"
	Então o sistema deve retornar o status 409 Conflict

# --- Regras - Editar e Inativar Usuários ---
Cenário: Administrador edita ou inativa um usuário com sucesso
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário com ID 2
	Quando eu solicitar a <operacao> do usuário 2
	Então o sistema deve retornar o status 200 Ok
Exemplos:
	| operacao   |
	| edição     |
	| inativação |

Cenário: Erro ao editar usuário com dados inválidos
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário com ID 2
	Quando eu tentar editar o usuário 2 trocando o <campo> para o valor "<valor>"
	Então o sistema deve retornar o status 400 BadRequest
Exemplos:
	| campo  | valor          |
	| e-mail | email_invalido |
	| senha  | sfraca         |

Cenário: Erro ao editar usuário para um e-mail já existente
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário com ID 2 (usuário sendo editado)
	E já existe outro usuário cadastrado com o e-mail "existente@fiap.com"
	Quando eu tentar editar o usuário 2 trocando seu e-mail para "existente@fiap.com"
	Então o sistema deve retornar o status 409 Conflict

Cenário: Erro ao tentar editar ou inativar um usuário que não existe
	Dado que eu estou autenticado como "Administrador"
	E não existe um usuário com ID 999
	Quando eu tentar <operacao> o usuário 999
	Então o sistema deve retornar o status 404 NotFound
Exemplos:
	| operacao |
	| editar   |
	| inativar |

# --- Regras - Visualizar Dados ---
Cenário: Administrador visualiza usuário por ID ou E-mail
	Dado que eu estou autenticado como "Administrador"
	E existe um usuário cadastrado com ID 2 e e-mail "alvo@fiap.com"
	Quando eu buscar pelo identificador <tipo_busca>
	Então o sistema deve retornar o status 200 Ok
Exemplos:
	| tipo_busca |
	| "ID 2"     |
	| "e-mail"   |

# --- Regras - Restrições de Acesso do Usuário Comum ---
Cenário: Usuário comum tenta realizar operação proibida
	Dado que eu estou autenticado como "Usuário Comum"
	Quando eu tentar <operacao>
	Então o sistema deve recusar o acesso com o status 403 Forbidden
Exemplos:
	| operacao                    |
	| "listar todos os usuários"  |
	| "editar o usuário 2"        |
	| "inativar o usuário 2"      |
	| "buscar o usuário por ID 2" |