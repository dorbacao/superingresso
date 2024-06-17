import toastr from "./../components/toast";

function _try(action, value){
    try{
        return action();
    }catch(ex){
        return value;
    }
}

function groupBy(key) {
    return function group(array) {
      return array.reduce((acc, obj) => {
        const property = obj[key];
        acc[property] = acc[property] || [];
        acc[property].push(obj);
        return acc;
      }, {});
    };
  }

const MessageType = {
    Success: 1,
    Error: 2,
    Warn: 3,
    Info: 4,
    CriticalError: 5
  };

function getTextFromArray(messageArray){
    var errorMessage = '';

    messageArray.map(function(value, key) {
      errorMessage += value.text + '\n';                
    });

    return errorMessage;
}

const config ={
    gravity: 'top',
    position: 'center'
};

const answer = {

    showSuccess(successMessage){
        toastr.success(successMessage, config);
    },

    showInfo(infoMessage){
        toastr.info(infoMessage,config);
    },

    showWarn(infoMessage){
        toastr.warning(infoMessage, config);
    },

    showError(infoMessage){
        toastr.danger(infoMessage, config);
    },

    fromSuccess(successMessageArray){
        toastr.success(getTextFromArray(successMessageArray),config);
    },

    fromError(successMessageArray){
        toastr.danger(getTextFromArray(successMessageArray),config);
    },

    fromWarn(successMessageArray){
        toastr.warning(getTextFromArray(successMessageArray),config);
    },

    fromInfo(successMessageArray){
        toastr.info(getTextFromArray(successMessageArray), config);
    },

    fromContract(messages){
       //não faz sentido exibir muitas mensages ao utilizador
       var filteredMessages = messages.filter((item, index) => index < 20);

       var reduceByGroup = groupBy('type');
       var groupArrayByType = reduceByGroup(filteredMessages);

       var messagesSuccess = groupArrayByType[MessageType.Success];
       var messagesError = groupArrayByType[MessageType.Error];
       var messagesWarn = groupArrayByType[MessageType.Warn];
       var messagesInfo = groupArrayByType[MessageType.Info];
       var messagesCritical = groupArrayByType[MessageType.CriticalError];

       if(messagesSuccess) this.fromSuccess(messagesSuccess);
       if(messagesError) this.fromError(messagesError);
       if(messagesWarn) this.fromWarn(messagesWarn);
       if(messagesInfo) this.fromInfo(messagesInfo);
       if(messagesCritical) this.fromError(messagesCritical);
    },

    getListErrors(messages){
        var reduceByGroup = groupBy('type');
        var groupArrayByType = reduceByGroup(messages);
 
        var messagesError = groupArrayByType[MessageType.Error];
        var messagesCritical = groupArrayByType[MessageType.CriticalError];

        if(!messagesError) messagesError = [];
        if(!messagesCritical) messagesCritical = [];

        var allErros = messagesError.concat(messagesCritical);

        console.log('allErros', allErros);
        return allErros;
     },

    fromResponse(data){
        var isAnswerContractOk = _try(() => data.messages, false); //contrato quando for uma resposta 200
        var isAnswerContractError = _try(() => data.response.messages, false); //contrato quando for uma resposta 400/500

        if(isAnswerContractOk)
        {
            this.fromContract(isAnswerContractOk);
        }
        else if(isAnswerContractError){
            console.log('iserror');
            this.fromContract(isAnswerContractError);
        }
        
    },

    fromException(exception){
        this.showError(exception.message);
    },

    getListErrorsFromResponse(data){
        var isAnswerContractError = _try(() => data.response.data.messages, false); //contrato quando for uma resposta 400/500

        if(isAnswerContractError){
            return this.getListErrors(isAnswerContractError);
        }
        
    },
}

export default answer;