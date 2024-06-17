
window.$ = {
    id(id){
        return document.getElementById(id);
    },
    addEvent(id, eventName, callback){
        var element = this.id(id);
        if(element) {
            element.addEventListner(eventName, callback);
        }else{
            console.error('element not found');
        }            
    },
    create(name){
        return document.createElement(name);        
    }
};

export default {
    init(){

    }
}