#language: pt-br
Funcionalidade: Autenticação de Usuários
  Como um usuário cadastrado
  Quero fornecer minhas credenciais
  Para receber um token de acesso e utilizar os endpoints liberados para o meu perfil

  Contexto: 
    Dado que existe um usuário cadastrado com e-mail "usuario@fiap.com" e senha "Senha@123"

# --- Regras - Autenticação ---
  Cenário: Login com sucesso e geração de token
    Dado que eu informo o e-mail "usuario@teste.com"
    E a senha "Senha@123"
    Quando eu solicitar o login
    Então o sistema deve retornar o status 200 OK
    E deve conter um token JWT válido na resposta

  Cenário: Erro ao tentar login com senha incorreta
    Dado que eu informo o e-mail "usuario@teste.com"
    E a senha "SenhaErrada!"
    Quando eu solicitar o login
    Então o sistema deve retornar o status 401 Unauthorized

  Cenário: Erro ao tentar login com usuário inexistente
    Dado que eu informo o e-mail "nao_existo@teste.com"
    E a senha "QualquerSenha123"
    Quando eu solicitar o login
    Então o sistema deve retornar o status 401 Unauthorized
