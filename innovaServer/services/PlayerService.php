<?php

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

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
        
    }
    
}

new PlayerService();

?>
