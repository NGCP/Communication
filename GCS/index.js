const SerialPort = require('serialport');
const { constants: C, XBeeAPI } = require('xbee-api');

const SELF_XBEE_PORT = '/dev/tty.usbserial-DA01R7GR';
const DESTINATION_XBEE_PORT = '0013A2004067E4AE';

const port = new SerialPort(SELF_XBEE_PORT, { baudRate: 57600 });
const xbeeAPI = new XBeeAPI({ api_mode: 1 });

const data = {
  latitude: 69,
  longitude: 10,
}

port.pipe(xbeeAPI.parser);
xbeeAPI.builder.pipe(port);

port.on('open', () => {
  setInterval(() => {
    xbeeAPI.builder.write({
      type: C.FRAME_TYPE.ZIGBEE_TRANSMIT_REQUEST,
      destination64: DESTINATION_XBEE_PORT,
      data: JSON.stringify(data),
    });
  }, 5000);
});

xbeeAPI.parser.on('data', frame => {
  if (frame.type !== C.FRAME_TYPE.ZIGBEE_RECEIVE_PACKET) return;

  const json = frame.data.toString();
  console.log(JSON.parse(json));
});
