# 🚀 API Fiap Cloud Games

> API desenvolvida para gerenciamento de usuários e jogos da Plataforma FIAP Cloud Games.

[![Licença](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)
[![Status do Projeto](https://img.shields.io/badge/Status-Em%20Desenvolvimento-green)](#)

---

## 💻 Sobre o projeto

A **API Fiap Cloud Games** é um projeto MVP que tem como objetivo gerenciar usuários e jogos cadastrados na plataforma FIAP Cloud Games.

## 🛠 Tecnologias utilizadas

Este projeto foi desenvolvido utilizando as seguintes tecnologias:

* .NET 8.0
* Entity Framework Core
* MariaDB (MySQL)

## ⚙️ Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
* [.NET Core 8.0.25](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)
* [MariaDB](https://mariadb.org/download/?t=mariadb&p=mariadb&r=11.4.10&os=windows&cpu=x86_64&pkg=msi&mirror=fder) (versão 11.4 ou superior)

## 🚀 Como executar o projeto

Siga os passos abaixo para rodar o projeto localmente:

### 1. Clone o repositório
```bash
git clone https://github.com/splinterxsr/FiapCloudGames.git
```
### 2. Criar o banco de dados
```
CREATE DATABASE fiapcloud;
```
### 3. Criar o usuário admin
```
CREATE USER 'admin'@'localhost' IDENTIFIED BY 'admin';
```
### 4. Atribuir permissão ao usuário admin
```
GRANT ALL PRIVILEGES ON fiapcloud.* TO 'admin'@'localhost';
```
### 5. Abra o projeto e rode a migration
```bash
Update-Database
```
