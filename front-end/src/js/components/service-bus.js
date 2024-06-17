// Modelo Pub/Sub
class PubSub {
    constructor() {
      this.subscribers = {};
    }
  
    // Método para publicar eventos
    publish(event, data) {
      if (!this.subscribers[event]) {
        this.subscribers[event] = [];
      }
      this.subscribers[event].forEach(subscriber => subscriber(data));
    }
  
    // Método para assinar eventos
    subscribe(event, callback) {
      if (!this.subscribers[event]) {
        this.subscribers[event] = [];
      }
      this.subscribers[event].push(callback);
    }
  
    // Método para desassinar eventos
    unsubscribe(event, callback) {
      if (!this.subscribers[event]) {
        return;
      }
      const index = this.subscribers[event].indexOf(callback);
      if (index > -1) {
        this.subscribers[event].splice(index, 1);
      }
    }
  }  
  
export default {
  init(){
    window.$bus = new PubSub();
    return window.$bus;
  }  
}
  // Exemplo de uso
  
  
//   // Assinando um evento
//   pubSub.subscribe('evento', (data) => {
//     console.log('Evento recebido:', data);
//   });
  
//   // Publicando um evento
//   pubSub.publish('evento', { mensagem: 'Olá, mundo!' });
  
//   // Desassinando o evento
//   pubSub.unsubscribe('evento', (data) => {
//     console.log('Evento recebido:', data);
//   });
  