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


