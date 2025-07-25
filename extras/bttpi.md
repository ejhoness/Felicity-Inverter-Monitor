BTTpiv.12 with scrren
at 12v. 
power system ip, ssh, login..., no screen , flip flat wires :P
flat wire and disabled another overlays scrren
one line overlays
overlays=tft35_spi mcp2515 ws2812 
but now :)

klipper screen  tnks to all to all
blz, quero mostrar outro scrren ?

i2cdetect -l
4 ativas 
i2c-0	i2c       	mv64xxx_i2c adapter             	I2C adapter
i2c-1	i2c       	DesignWare HDMI                 	I2C adapter
i2c-2	i2c       	mv64xxx_i2c adapter             	I2C adapter
i2c-3	i2c       	i2c-gpio                        	I2C adapter
scan serial ports
finding pins to use uart ttl
8 10 

now try to install ath** module temperatura humidade

http://192.168.1.103:8123 home assistant

sudo nano /etc/pihole/pihole.toml
8080
http://192.168.1.103:8080 pihole

http://192.168.1.103 klipper

http://192.168.1.2:9090/system

http://192.168.1.2 klipper

http://192.168.1.2:8080/admin/ pihole

http://192.168.1.2:8123 home assistante

http://192.168.1.2:8081 Felicity-Inverter-Monitor



change ip to wired and fix .103 wifi

another controller to expand port pins to make more easy pinout usb rp 2040?

new day

today 24-07-25 power wires on install power line and data cables, wifi conection, 

ls -l /dev/ttyS0 e ls -l /dev/ttyUSB0
sudo usermod -a -G dialout $USER
sudo apt-get install screen

ls -l /dev/serial*
ls -l /dev/ttyS*

dmesg | grep tty
sudo nano /boot/armbianEnv.txt
sudo shutdown -r now

i try to usb ttl to make ttyUSB1

i not sure ttyS0 fuctional, not find txrx :(

https://github.com/hoylabs/OpenDTU-OnBattery

new again

192.168.1.2 mainsail
192.168.1.2:81 fluid
192.168.1.2:8080 pihole
192.168.1.2:8081 invertmoon
192.168.1.2:8123 home assistant
192.168.1.2:9090 cockpit

need adjust swap memory ? less 512mb ? change to 2 gb. swap memory chip to chips 2gb ?
install ftdi to give anhoter port 

lsusb                                                                                                                                         <master>
Bus 007 Device 001: ID 1d6b:0001 Linux Foundation 1.1 root hub
Bus 004 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 006 Device 002: ID 1a86:7523 QinHeng Electronics CH340 serial converter
Bus 006 Device 001: ID 1d6b:0001 Linux Foundation 1.1 root hub
Bus 003 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 005 Device 001: ID 1d6b:0001 Linux Foundation 1.1 root hub
Bus 002 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub
Bus 008 Device 002: ID 0403:6001 Future Technology Devices International, Ltd FT232 Serial (UART) IC
Bus 008 Device 001: ID 1d6b:0001 Linux Foundation 1.1 root hub
Bus 001 Device 001: ID 1d6b:0002 Linux Foundation 2.0 root hub

ttyUSB0
ttyUSB1

screen
tanks Gemini
20210608B.1.1.pdf
Create trans_pdf_felicitycomunication.md
[Protocolo de Comunicação do Inversor](trans_pdf_felicitycomunication.md)
