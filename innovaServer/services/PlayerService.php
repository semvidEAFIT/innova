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
        if(Service::checkParamPOST('Player')){
            return $this->registerPlayer(json_decode(str_replace("+", " ", $_POST['Player'])));
        }else if(Service::checkParamGET('topLength')){
            return $this->getTopPlayers($_GET['topLength']);
        }else{
            return $this->getPlayers();
        }
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
}

new PlayerService();

?>
