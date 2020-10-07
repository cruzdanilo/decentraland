!function(e,t){for(var r in t)e[r]=t[r]}(this,function(e){var t={};function r(s){if(t[s])return t[s].exports;var i=t[s]={i:s,l:!1,exports:{}};return e[s].call(i.exports,i,i.exports,r),i.l=!0,i.exports}return r.m=e,r.c=t,r.d=function(e,t,s){r.o(e,t)||Object.defineProperty(e,t,{enumerable:!0,get:s})},r.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},r.t=function(e,t){if(1&t&&(e=r(e)),8&t)return e;if(4&t&&"object"==typeof e&&e&&e.__esModule)return e;var s=Object.create(null);if(r.r(s),Object.defineProperty(s,"default",{enumerable:!0,value:e}),2&t&&"string"!=typeof e)for(var i in e)r.d(s,i,function(t){return e[t]}.bind(null,i));return s},r.n=function(e){var t=e&&e.__esModule?function(){return e.default}:function(){return e};return r.d(t,"a",t),t},r.o=function(e,t){return Object.prototype.hasOwnProperty.call(e,t)},r.p="",r(r.s=2)}([function(e,t,r){"use strict";function s(){let e,t;const r=new Promise((s,i)=>{e=e=>{s(e),r.isPending=!1},t=e=>{i(e),r.isPending=!1}}).catch(e=>Promise.reject(e));return r.resolve=e,r.reject=t,"finally"in r||(r.finally=e=>{r.then(e),r.catch(e)}),r.isPending=!0,r}Object.defineProperty(t,"__esModule",{value:!0}),t.future=s,t.default=s},function(e,t,r){"use strict";r.r(t);r(0);class s{constructor(e,t){this.size=e,this.ArrayTypeConstructor=t,this.writePointer=0,this.readPointer=0,this.buffer=new this.ArrayTypeConstructor(e)}readAvailableCount(){return this.writePointer-this.readPointer}getWritePointer(){return this.writePointer}getReadPointer(){return this.readPointer}write(e,t){this.writeAt(e,this.writePointer,t)}read(e){const t=this.peek(this.readPointer,e);return this.readPointer+=t.length,t}peek(e,t){const r=e||this.readPointer,s=this.writePointer-this.readPointer,i=t?Math.min(t,s):s,n=r%this.buffer.length,o=n+i;let a;return o>this.buffer.length?(a=new this.ArrayTypeConstructor(i),a.set(this.buffer.slice(n,this.buffer.length)),a.set(this.buffer.slice(0,o-this.buffer.length),this.buffer.length-n)):a=this.buffer.slice(n,o),a}writeAt(e,t,r){const s=r||e.length;let i=e;s>this.buffer.length&&(i=e.slice(e.length-this.buffer.length,e.length));const n=t%this.buffer.length;if(n+s>this.buffer.length){const e=this.buffer.length-n;this.buffer.set(i.slice(0,e),n),this.buffer.set(i.slice(e,s),0)}else this.buffer.set(i.slice(0,s),n);const o=t+s;o>this.writePointer&&(this.writePointer=o),this.updateReadPointerToMinReadPosition()}isFull(){return this.readAvailableCount()>=this.size}updateReadPointerToMinReadPosition(){const e=this.writePointer-this.buffer.length;this.readPointer<e&&(this.readPointer=e)}}var i,n,o,a,u;!function(e){e.ENCODE="ENCODE",e.DECODE="DECODE",e.DESTROY_ENCODER="DESTROY_ENCODER",e.DESTROY_DECODER="DESTROY_ENCODER"}(i||(i={})),function(e){e.ENCODE="ENCODE",e.PAUSE="PAUSE",e.RESUME="RESUME",e.ON_PAUSED="ON_PAUSED",e.ON_RECORDING="ON_RECORDING"}(n||(n={})),function(e){e.STREAM_PLAYING="STREAM_PLAYING",e.WRITE_SAMPLES="WRITE_SAMPLES"}(o||(o={})),function(e){e.ENCODE="ENCODE_OUTPUT",e.DECODE="DECODE_OUTPUT"}(a||(a={})),function(e){e[e.RECORDING=0]="RECORDING",e[e.PAUSE_REQUESTED=1]="PAUSE_REQUESTED",e[e.PAUSED=2]="PAUSED"}(u||(u={}));class l extends AudioWorkletProcessor{constructor(...e){super(...e),this.status=u.PAUSED,this.inputSamplesCount=0,this.port.onmessage=e=>{e.data.topic===n.PAUSE&&(this.status=u.PAUSE_REQUESTED),e.data.topic===n.RESUME&&(this.status=u.RECORDING,this.notify(n.ON_RECORDING))}}process(e,t,r){if(this.status===u.PAUSED)return!0;let s=e[0][0];if(this.status===u.PAUSE_REQUESTED){const e=1440*Math.floor(s.length/1440)+1440-this.inputSamplesCount%1440;s=s.slice(0,e),this.status=u.PAUSED,this.notify(n.ON_PAUSED)}return this.sendDataToEncode(s),this.inputSamplesCount+=s.length,!0}notify(e){this.port.postMessage({topic:e})}sendDataToEncode(e){this.port.postMessage({topic:n.ENCODE,samples:e},[e.buffer])}}class f extends AudioWorkletProcessor{constructor(e){var t,r;super(e),this.playing=!1,this.bufferLength=null!==(t=null==e?void 0:e.processorOptions.channelBufferSize)&&void 0!==t?t:2,this.sampleRate=null!==(r=null==e?void 0:e.processorOptions.sampleRate)&&void 0!==r?r:24e3,this.buffer=new s(Math.floor(this.bufferLength*this.sampleRate),Float32Array),this.port.onmessage=e=>{e.data.topic===o.WRITE_SAMPLES&&this.buffer.write(e.data.samples)}}process(e,t,r){const s=t[0][0];s.fill(0);const i=this.playing,n=i?0:s.length-1;return this.buffer.readAvailableCount()>n?(s.set(this.buffer.read(s.length)),i||this.changePlayingStatus(!0)):i&&this.changePlayingStatus(!1),!0}changePlayingStatus(e){this.playing=e,this.port.postMessage({topic:o.STREAM_PLAYING,playing:e})}}registerProcessor("inputProcessor",l),registerProcessor("outputProcessor",f)},function(e,t,r){"use strict";r.r(t);const s=r(1);var i;s&&s.__esModule&&s.default&&new s.default((i=self,{onConnect(e){i.addEventListener("message",()=>e(),{once:!0})},onError(e){i.addEventListener("error",t=>{t.error?e(t.error):t.message&&e(Object.assign(new Error(t.message),{colno:t.colno,error:t.error,filename:t.filename,lineno:t.lineno,message:t.message}))})},onMessage(e){i.addEventListener("message",t=>{e(t.data)})},sendMessage(e){i.postMessage(e)},close(){"terminate"in i?i.terminate():"close"in i&&i.close()}}))}]));