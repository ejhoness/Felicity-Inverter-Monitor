# Protocolo de Comunicação do Inversor de Alta Frequência (Felicity Communication)

Este documento detalha o protocolo de comunicação UART Modbus para o inversor de alta frequência, conforme especificado no manual.

---

## [cite_start]1. Definição da Interface de Comunicação [cite: 2]

* [cite_start]**Tipo de Comunicação:** UART (Universal Asynchronous Receiver/Transmitter) [cite: 3]
* [cite_start]**Configurações de Comunicação:** [cite: 3]
    * [cite_start]**Baud Rate (Taxa de Baud):** 2400 bps [cite: 3]
    * [cite_start]**Data Bits (Bits de Dados):** 8 bits [cite: 3]
    * [cite_start]**Stop Bits (Bits de Parada):** 1 bit [cite: 3]
    * [cite_start]**Parity (Paridade):** Nenhuma [cite: 3]
    * [cite_start]**Flow Control (Controle de Fluxo):** Nenhum [cite: 3]
* [cite_start]**Modo de Comunicação:** Half-duplex (meio-duplex)[cite: 3].
    * [cite_start]Neste modo, apenas um dispositivo (Host/Mestre ou Inversor/Escravo) pode enviar dados por vez, enquanto o outro recebe[cite: 3].
    * [cite_start]A comunicação é sempre iniciada pelo controlador externo (Host/Mestre), e o controlador do inversor (Escravo) responde (não inicia a comunicação proativamente)[cite: 3].
* [cite_start]**Protocolo de Comunicação:** MODBUS Protocol Frames[cite: 3].
* [cite_start]**Endereço do Escravo (Inversor):** Faixa de 1 a 31 (decimal), onde 31 é o endereço de transmissão (broadcast)[cite: 5].

---

## [cite_start]2. Definição da Estrutura do Frame de Comunicação [cite: 4]

A estrutura do frame de comunicação segue o protocolo MODBUS. [cite_start]A CRC (Cyclic Redundancy Check) de 16 bits é calculada a partir do endereço até o campo CRC (excluindo o próprio campo CRC)[cite: 7].

### [cite_start]Códigos de Função (Comandos) [cite: 5]

* [cite_start]`0x03`: Ler Múltiplos Registradores (parâmetros) [cite: 5]
* [cite_start]`0x06`: Escrever um Único Registrador (parâmetro) [cite: 5]
* [cite_start]`0x10`: Escrever Múltiplos Registradores (parâmetros) [cite: 5]
* [cite_start]`0x17`: Sincronização de Dados Mestre-Escravo [cite: 5]
* [cite_start]`0x41`: Atualização de Firmware [cite: 5]

---

## 2.1. [cite_start]Comandos do Frame de Comunicação e Descrição [cite: 6]

### 2.1.1. [cite_start]`0x03` Ler Múltiplos Registradores [cite: 8]

Este código de função é usado para ler o conteúdo de um bloco contínuo de registradores. [cite_start]A unidade de dados do protocolo de requisição especifica o endereço inicial do registrador e a quantidade de registradores a serem lidos[cite: 9].

* [cite_start]**Formato de Dados:** Na resposta, cada registrador contém dois bytes (números binários alinhados à direita em cada byte)[cite: 9]. [cite_start]O primeiro byte é o byte de ordem alta, e o segundo byte é o byte de ordem baixa[cite: 9].

* [cite_start]**Exemplo de Requisição e Resposta (Registradores 0x0001-0x0002):** [cite: 10]

    | Requisição (Hex)                  | Resposta (Hex)                  |
    | :-------------------------------- | :------------------------------ |
    | `Slave Address`: `01`             | `Slave Address`: `01`           |
    | `Command`: `03`                   | `Command`: `03`                 |
    | `Register Start Address High`: `00` | `Byte Count`: `04`              |
    | `Register Start Address Low`: `01`  | `Register Value High (01)`: `0F` |
    | `Number of Registers High`: `00`  | `Register Value Low (01)`: `AD` |
    | `Number of Registers Low`: `02`   | `Register Value High (02)`: `01` |
    | `CRC Low`                         | `Register Value Low (02)`: `C2` |
    | `CRC High`                        | `CRC Low`                       |
    |                                   | `CRC High`                      |

### 2.1.2. [cite_start]`0x06` Escrever um Único Registrador [cite: 11]

Este código de função é usado para escrever um registrador de retenção (holding register) em um dispositivo escravo. [cite_start]A requisição especifica o endereço do registrador a ser escrito e o valor a ser gravado[cite: 12]. [cite_start]A resposta normal é uma cópia da requisição, confirmando que o conteúdo do registrador foi atualizado[cite: 12].

* [cite_start]**Exemplo de Requisição e Resposta (Escrever valor `0xAAAA` no Registrador `0x0008`):** [cite: 13]

    | Requisição (Hex)                  | Resposta (Hex)                  |
    | :-------------------------------- | :------------------------------ |
    | `Slave Address`: `01`             | `Slave Address`: `01`           |
    | `Command`: `06`                   | `Command`: `06`                 |
    | `Register Start Address High`: `00` | `Register Start Address High`: `00` |
    | `Register Start Address Low`: `08`  | `Register Start Address Low`: `08`  |
    | `Register Value High`: `AA`       | `Register Value High`: `AA`       |
    | `Register Value Low`: `AA`        | `Register Value Low`: `AA`        |
    | `CRC Low`                         | `CRC Low`                       |
    | `CRC High`                        | `CRC High`                      |

### 2.1.3. [cite_start]`0x10` Escrever Múltiplos Registradores [cite: 14]

Este código de função é usado para escrever uma série de valores em endereços contínuos de registradores. Os valores a serem escritos são especificados no campo de dados da requisição. Os dados são em palavras de dois bytes por registrador. [cite_start]A resposta normal retorna o código de função, o endereço inicial e a quantidade de registradores escritos[cite: 15].

* [cite_start]**Exemplo de Requisição e Resposta (Escrever `0x1194` no `0x0001` e `0x01CC` no `0x0002`):** [cite: 16]

    | Requisição (Hex)                  | Resposta (Hex)                  |
    | :-------------------------------- | :------------------------------ |
    | `Slave Address`: `01`             | `Slave Address`: `01`           |
    | `Command`: `10`                   | `Command`: `10`                 |
    | `Register Start Address High`: `00` | `Register Start Address High`: `00` |
    | `Register Start Address Low`: `01`  | `Register Start Address Low`: `01`  |
    | `Number of Registers High`: `00`  | `Number of Registers High`: `00` |
    | `Number of Registers Low`: `02`   | `Number of Registers Low`: `02` |
    | `Byte Count`: `04`                | `CRC Low`                       |
    | `Register Value High (01)`: `11` | `CRC High`                      |
    | `Register Value Low (01)`: `94`  |                                 |
    | `Register Value High (02)`: `01` |                                 |
    | `Register Value Low (02)`: `CC`  |                                 |
    | `CRC Low`                         |                                 |
    | `CRC High`                        |                                 |

---

## [cite_start]3. Definição dos Registradores de Dados [cite: 17]

### 3.1. [cite_start]Definição dos Registradores de Dados de Informação [cite: 18] (Leitura)

Estes são registradores somente para leitura (R) que contêm informações sobre o dispositivo.

| Endereço (Hex) | Tamanho (Word) | Nome do Registrador    | Tipo   | Acesso | Descrição do Registrador         | Observações
