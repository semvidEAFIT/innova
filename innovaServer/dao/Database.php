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
        $this->server = 'semvidInnova.db.10806069.hostedresource.com';
        $this->databaseName = 'semvidInnova';
        $this->user = 'semvidInnova';
        $this->password = 'Innova2013!';
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
