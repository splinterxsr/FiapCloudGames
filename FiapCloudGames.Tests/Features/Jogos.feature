#language: pt-br
Funcionalidade: Gestão de Jogos
  Como uma API de catálogo de jogos
  Quero que as operações de escrita sejam restritas a administradores
  Enquanto a visualização é permitida para todos os usuários logados

# --- Regras - Adicionar Jogos ---
Cenário: Administrador adiciona um novo jogo com sucesso
	Dado que eu estou autenticado como "Administrador"
	E envio os dados válidos de um novo jogo
	Quando eu solicitar o cadastro do jogo
	Então o sistema deve retornar o status 201 Created
	E o jogo deve estar salvo no banco de dados

Cenário: Erro ao adicionar jogo com dados inválidos
	Dado que eu estou autenticado c omo "Administrador"
	Quando eu tentar adicionar um jogo sem o campo obrigatório "<campo>"
	Então o sistema deve retornar o status 400 BadRequest
Exemplos:
	| campo |
	| Nome  |

# --- Regras - Editar e Inativar Jogos ---
Cenário: Administrador edita ou inativa um jogo existente
	Dado que eu estou autenticado como "Administrador"
	E existe um jogo cadastrado com ID 1
	Quando eu solicitar a <operacao> do jogo 1
	Então o sistema deve retornar o status 200 Ok
Exemplos:
	| operacao  |
	| edição    |
	| inativação|

Cenário: Erro ao operar sobre um jogo inexistente
	Dado que eu estou autenticado como "Administrador"
	E não existe um jogo com ID 999
	Quando eu tentar <operacao> o jogo 999
	Então o sistema deve retornar o status 404 NotFound
Exemplos:
	| operacao |
	| editar   |
	| inativar |

# --- Regra: Visualizar Jogos ---
Cenário: Usuário visualiza informações dos jogos
	Dado que eu estou autenticado
	E existe um jogo cadastrado com ID 1
	Quando eu solicitar <acao>
	Então o sistema deve retornar o status 200 OK
Exemplos:
	| acao                      |
	| a lista de todos os jogos |
	| os detalhes do jogo 1     |

# --- Regras - Restrições de Acesso do Usuário Comum ---
Cenário: Usuário comum tenta realizar operação proibida
	Dado que eu estou autenticado como "Usuário Comum"
	Quando eu tentar <operacao>
	Então o sistema deve recusar o acesso com o status 403 Forbidden
Exemplos:
	| operacao              |
	| "adicionar jogo novo" |
	| "editar o jogo 1"     |
	| "inativar o jogo 1"   |