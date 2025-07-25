# Guia de Comunicação Modbus RTU com Inversor (Felicity Communication) via USB

Este guia detalha como estabelecer comunicação Modbus RTU com o inversor de alta frequência (modelo Felicity Communication), utilizando uma conexão serial via USB (`/dev/ttyUSBx`) em um sistema Linux.

---

## 1. Pré-requisitos e Ferramentas Necessárias

Antes de iniciar, certifique-se de ter o seguinte:

* **Sistema Operacional:** Linux (Ubuntu, Debian, Fedora, etc.).
* **Adaptador USB para Serial (RS-485 ou RS-232):** O inversor utiliza comunicação serial. Você precisará de um adaptador USB-to-RS485/RS232 para conectar o inversor ao seu computador.
* **Cabo de Comunicação:** O cabo apropriado para conectar o inversor ao adaptador serial.
* **Software `screen`:** Para testes rápidos de comunicação serial.
    * Instalação: `sudo apt install screen`
* **Biblioteca Python `pymodbus`:** Para desenvolvimento de scripts de comunicação mais robustos.
    * Instalação: `pip install pymodbus` (se não tiver `pip`, instale `sudo apt install python3-pip`)

---

## 2. Configurações da Comunicação Serial (UART)

O inversor de alta frequência (Felicity Communication) utiliza as seguintes configurações padrão para a comunicação UART Modbus:

* **Tipo de Comunicação:** UART (via porta serial virtual, e.g., `/dev/ttyUSB1`)
* **Baud Rate (Taxa de Baud):** `2400 bps`
* **Data Bits (Bits de Dados):** `8 bits`
* **Stop Bits (Bits de Parada):** `1 bit`
* **Parity (Paridade):** `Nenhuma`
* **Flow Control (Controle de Fluxo):** `Nenhum`
* **Modo de Comunicação:** Half-duplex (O host/mestre inicia a comunicação, e o inversor/escravo responde).
* **Endereço do Escravo (Inversor):** `1` a `31` (decimal), onde `31` é o endereço de broadcast.

---

## 3. Identificando a Porta Serial (ttyUSBx)

Ao conectar seu adaptador USB-to-Serial, o Linux atribui uma porta como `/dev/ttyUSB0`, `/dev/ttyUSB1`, etc. É crucial identificar a porta correta:

1.  **Conecte o Adaptador:** Conecte o adaptador USB-to-Serial ao seu computador (sem conectar o inversor ainda).
2.  **Verifique as Portas Atuais:** Abra um terminal e execute:
    ```bash
    ls /dev/ttyUSB*
    ```
    Isso listará quaisquer portas `ttyUSB` já existentes.
3.  **Conecte o Inversor ao Adaptador:** Agora, conecte o inversor ao adaptador serial.
4.  **Verifique Novamente:** Execute o comando `ls /dev/ttyUSB*` novamente. A nova porta que aparecer (ex: `/dev/ttyUSB1`) é provavelmente a do seu inversor.
5.  **Confirme com `dmesg`:** Para uma confirmação mais detalhada, você pode verificar o log do kernel logo após plugar o dispositivo:
    ```bash
    dmesg | grep tty
    ```
    Procure por linhas que indiquem o dispositivo USB sendo anexado a uma porta `ttyUSB`.

    **Exemplo de saída:**
    ```
    [ 123.456789] usb 1-1: FTDI USB Serial Device converter now attached to ttyUSB1
    ```

### Permissões da Porta Serial

Por padrão, usuários normais podem não ter permissão para acessar portas seriais. Você precisará estar no grupo `dialout`.

1.  **Verifique seus grupos:**
    ```bash
    groups $USER
    ```
    Verifique se `dialout` está na lista.
2.  **Adicione-se ao grupo (se necessário):**
    ```bash
    sudo usermod -a -G dialout $USER
    ```
    **IMPORTANTE:** Após adicionar-se ao grupo, você **deve fazer logout e login novamente** (ou reiniciar o computador) para que as novas permissões sejam aplicadas.

---

## 4. Testando a Comunicação com `screen`

O `screen` é útil para verificar se há algum dado sendo transmitido pela porta serial e para testar a conexão básica.

1.  **Abra uma sessão `screen`:**
    Substitua `/dev/ttyUSBx` pela sua porta identificada (ex: `/dev/ttyUSB1`) e `2400` pela Baud Rate.
    ```bash
    screen /dev/ttyUSB1 2400
    ```
2.  **Verifique a Conexão:**
    Se a conexão for bem-sucedida, o terminal ficará em branco. Se houver dados sendo enviados pelo inversor de forma autônoma, você os verá. Se não, você precisará de um mestre Modbus para *pedir* dados.
3.  **Comandos do `screen` (use `Ctrl+A` como prefixo):**
    * `Ctrl+A` `d`: Desanexar a sessão (ela continua rodando em segundo plano).
    * `Ctrl+A` `k`: Matar a sessão atual (fecha-a). Confirme com `y`.
    * `Ctrl+A` `?`: Exibir ajuda.
4.  **Reanexar uma sessão (se você a desanexou):**
    * Primeiro, liste as sessões ativas: `screen -ls`
    * Depois, reanexe: `screen -r [ID_da_sessao]` (ex: `screen -r 12345`)

**Nota:** O `screen` é um terminal bruto. Para comunicação Modbus real, onde você envia comandos específicos e recebe respostas estruturadas, você precisará de um cliente Modbus (como um script Python).

---

## 5. Obtendo Dados com Python e `pymodbus`

A biblioteca `pymodbus` é excelente para criar um cliente Modbus (mestre) que pode ler dados do seu inversor.

### Exemplo de Script Python para Leitura

Crie um arquivo Python (ex: `read_inverter_data.py`) com o seguinte conteúdo:

```python
#sudo apt update
#Instale o pip para Python 3 (recomendado):
# sudo apt install python3-pip
# Certifique-se de ter pymodbus instalado: pip install pymodbus

from pymodbus.client import ModbusSerialClient
import time

# --- Configurações da Porta Serial e Modbus ---
SERIAL_PORT = '/dev/ttyUSB1'  # **AJUSTE PARA SUA PORTA REAL**
BAUD_RATE = 2400
SLAVE_ID = 1                  # **AJUSTE PARA O ID DO SEU INVERSOR (geralmente 1)**

# --- Mapeamento de Registradores (conforme seu manual) ---
# Endereços em Hexadecimal no manual são convertidos para Decimal para uso
# com pymodbus (que usa endereços base 0).
# O manual indica 'Holding Registers' (Função 0x03), então usaremos read_holding_registers.

# Registradores de Dados de Informação (apenas leitura)
# Estes são 'Holding Registers' apesar dos endereços altos.
REG_DEVICE_TYPE = 0xF800 # 63488 Decimal
REG_SUB_TYPE = 0xF801    # 63489 Decimal
REG_SERIAL_NUMBER = 0xF804 # 63492 Decimal (5 words)
REG_CPU1_FW_VERSION = 0xF80B # 63499 Decimal
REG_CPU2_FW_VERSION = 0xF80C # 63500 Decimal

# Registradores de Dados em Tempo Real (apenas leitura)
REG_WORKING_MODE = 0x1101       # 4353 Decimal
REG_BATTERY_VOLTAGE = 0x1108    # 4360 Decimal (Volts)
REG_BATTERY_CURRENT = 0x1109    # 4361 Decimal (Amps, INT16S)
REG_BATTERY_POWER = 0x110A      # 4362 Decimal (Watts, INT16S)
REG_AC_OUTPUT_VOLTAGE = 0x1111  # 4369 Decimal (Volts)
REG_AC_INPUT_VOLTAGE = 0x1117   # 4375 Decimal (Volts)
REG_LOAD_PERCENTAGE = 0x1120    # 4384 Decimal (%)
REG_PV_INPUT_VOLTAGE = 0x1126   # 4390 Decimal (Volts)
REG_PV_INPUT_POWER = 0x112A     # 4394 Decimal (Watts, INT16S)


def read_modbus_registers(client, address, count, description):
    """Lê um bloco de registradores Modbus e imprime o resultado."""
    try:
        # A função read_holding_registers (FC 0x03) é usada para esses registradores
        # O endereço fornecido aqui é 0-based.
        # Se o seu inversor espera 40001 como endereço inicial, e ele está no manual como 0x1100,
        # você usa 0x1100 (4352) diretamente no pymodbus.
        response = client.read_holding_registers(address=address, count=count, slave=SLAVE_ID)

        if response.isError():
            print(f"Erro ao ler {description} (End: {hex(address)}): {response}")
            return None
        else:
            # O manual indica que o primeiro byte é o mais significativo (Big-Endian) para cada word
            # Pymodbus já lê 16-bit words como inteiros.
            # Para INT16S (signed), Python lida automaticamente se o valor é tratado como tal.
            # Para valores que são porcentagens ou tensões/correntes, pode ser necessário dividir por 10 ou 100
            # dependendo da resolução esperada (o manual não especifica fatores de escala).
            # Por exemplo, 240 para 24.0V, 150 para 15.0A, etc.

            print(f"{description} (End: {hex(address)}, Qtd: {count}):")
            if description == "Número de Série":
                # O número de série é composto por 5 palavras (10 bytes).
                # Pode exigir concatenação e decodificação especial se for texto.
                # Se for um número puro como o exemplo SN-01354820250001, ele pode estar em formato BCD ou apenas ser os dígitos em cada byte/word.
                # Para simplificar, vamos apenas imprimir os valores das palavras.
                print(f"  Raw Words: {response.registers}")
            elif description == "Modo de Trabalho":
                mode = response.registers[0]
                modes = {
                    0: "PowerOnMode", 1: "Standby Mode", 2: "BypassMode",
                    3: "BatteryMode", 4: "FaultMode", 5: "Line Mode"
                }
                print(f"  Valor: {mode} ({modes.get(mode, 'Desconhecido')})")
            elif description == "Tensão da Bateria":
                voltage = response.registers[0] / 10.0 # Assumindo resolução de 0.1V, comum em inversores
                print(f"  Valor: {voltage:.1f} V")
            elif description == "Corrente da Bateria":
                current = response.registers[0]
                # Se for INT16S e você quer ver como float, pode ser necessário dividir por fator de escala
                # O manual não especifica escala, mas 10 ou 100 é comum.
                # Para INT16S, se o valor lido for > 32767, é negativo (complemento de dois). Python cuida disso.
                print(f"  Valor: {current} A (Pode precisar de fator de escala/sinal)")
            elif description == "Potência da Bateria":
                power = response.registers[0]
                print(f"  Valor: {power} W (Pode precisar de fator de escala/sinal)")
            elif description == "Tensão de Saída AC":
                voltage = response.registers[0] / 10.0 # Assumindo 0.1V
                print(f"  Valor: {voltage:.1f} V")
            elif description == "Tensão de Entrada AC":
                voltage = response.registers[0] / 10.0 # Assumindo 0.1V
                print(f"  Valor: {voltage:.1f} V")
            elif description == "Porcentagem de Carga":
                percentage = response.registers[0] / 10.0 # Assumindo 0.1%
                print(f"  Valor: {percentage:.1f} %")
            elif description == "Tensão de Entrada PV":
                voltage = response.registers[0] / 10.0 # Assumindo 0.1V
                print(f"  Valor: {voltage:.1f} V")
            elif description == "Potência de Entrada PV":
                power = response.registers[0]
                print(f"  Valor: {power} W (Pode precisar de fator de escala/sinal)")
            else:
                print(f"  Valores brutos: {response.registers}")
            return response.registers
    except Exception as e:
        print(f"Exceção ao ler {description} (End: {hex(address)}): {e}")
        return None

# --- Inicializa o Cliente Modbus Serial ---
client = ModbusSerialClient(
    port=SERIAL_PORT,
    baudrate=BAUD_RATE,
    bytesize=8,
    parity='N',
    stopbits=1,
    timeout=1 # Timeout em segundos
)

print(f"Tentando conectar ao inversor Modbus RTU em {SERIAL_PORT}...")
if client.connect():
    print("Conexão estabelecida com sucesso!")

    print("\n--- Lendo Dados de Informação ---")
    read_modbus_registers(client, REG_DEVICE_TYPE, 1, "Tipo de Dispositivo")
    read_modbus_registers(client, REG_SUB_TYPE, 1, "Subtipo de Dispositivo")
    read_modbus_registers(client, REG_SERIAL_NUMBER, 5, "Número de Série")
    read_modbus_registers(client, REG_CPU1_FW_VERSION, 1, "Versão FW CPU1")
    read_modbus_registers(client, REG_CPU2_FW_VERSION, 1, "Versão FW CPU2")

    print("\n--- Lendo Dados em Tempo Real ---")
    # Loop para leitura contínua de dados em tempo real
    try:
        while True:
            print(f"\n--- Leitura em tempo real (Slave ID: {SLAVE_ID}) ---")
            read_modbus_registers(client, REG_WORKING_MODE, 1, "Modo de Trabalho")
            read_modbus_registers(client, REG_BATTERY_VOLTAGE, 1, "Tensão da Bateria")
            read_modbus_registers(client, REG_BATTERY_CURRENT, 1, "Corrente da Bateria")
            read_modbus_registers(client, REG_BATTERY_POWER, 1, "Potência da Bateria")
            read_modbus_registers(client, REG_AC_OUTPUT_VOLTAGE, 1, "Tensão de Saída AC")
            read_modbus_registers(client, REG_AC_INPUT_VOLTAGE, 1, "Tensão de Entrada AC")
            read_modbus_registers(client, REG_LOAD_PERCENTAGE, 1, "Porcentagem de Carga")
            read_modbus_registers(client, REG_PV_INPUT_VOLTAGE, 1, "Tensão de Entrada PV")
            read_modbus_registers(client, REG_PV_INPUT_POWER, 1, "Potência de Entrada PV")

            print("\nAguardando 5 segundos para a próxima leitura...")
            time.sleep(5) # Lê a cada 5 segundos

    except KeyboardInterrupt:
        print("\nLeitura interrompida pelo usuário.")
    except Exception as e:
        print(f"Ocorreu um erro durante a leitura contínua: {e}")

    # --- Fecha a Conexão ---
    client.close()
    print("Conexão Modbus fechada.")
else:
    print("Falha ao conectar ao inversor. Verifique a porta serial, as permissões e o inversor.")
