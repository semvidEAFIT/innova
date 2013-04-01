<?php

include ('Service.php');
/**
 * Description of PlayerService
 *
 * @author Camilo
 */
class PlayerService extends Service{
    
    public function includeSpecificFiles(){
        include('../model/Player.php');
    }

    public function callService() {
        if(Service::checkParamPOST('Player')&&Service::checkParamPOST('Service')){
            if($_POST['Service'] == "Register"){
              $ivanddata = $_POST['Player'];
              $iv = substr($ivanddata, 0, 16);
              $data = substr($ivanddata, 16);
              $key = "04B915BA43FEB5B6";
              $decryptedData = trim(mcrypt_decrypt(MCRYPT_BLOWFISH, hex2bin($key), hex2bin($data), MCRYPT_MODE_CBC, hex2bin($iv)));
              return $this->registerPlayer(json_decode($decryptedData));
            }
        }else if(Service::checkParamGET('Service')){
            if($_GET['Service'] == "Ranking" && Service::checkParamGET('topLength')){
                return $this->getTopPlayers($_GET['topLength']);
            }else if($_GET['Service'] == "PlayerRanking"){
                return $this->getPlayerRanking($_GET['document']);
            }else if($_GET['Service'] == "List"){
                return $this->getPlayers();
            }
        }
        return getErrorArray("02", "El servicio no existe");
    }
    
    private function getTopPlayers($topLength){
        return ArrayHelper::toArray(Controller::getInstance()->getTopPlayers($topLength));
    }
    
    private function registerPlayer($Player){
        $player = Player::getPlayerObject($Player);
        return array('id' => Controller::getInstance()->registerPlayer($player));
    }
    
    private function getPlayers(){
        return ArrayHelper::toArray(Controller::getInstance()->getPlayers());
    }
    
    private function getPlayerRanking($document){
        return array('ranking' => Controller::getInstance()->getPlayerRanking($document));
    }
    
}

new PlayerService();

?>
