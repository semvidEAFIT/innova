<?php

/**
 * Current Database information
 */
class Database {

    private static $database;

    public static function getInstance(){
       if (Database::$database == null){
           Database::$database = new Database();
       }
       return Database::$database;
    }

    private $server;
    private $databaseName;
    private $user;
    private $password;

    private function __construct() {
        $this->server = 'localhost';
        $this->databaseName = 'noticias_eafit';
        $this->user = 'root';
        $this->password = '';
    }

    public function getServer() {
        return $this->server;
    }

    public function getDatabaseName() {
        return $this->databaseName;
    }

    public function getUser() {
        return $this->user;
    }

    public function getPassword() {
        return $this->password;
    }

}

?>
