# Greeting App

[![.NET](https://github.com/AnthonyMogotlane/greeting-app-csharp/actions/workflows/unit-test.yml/badge.svg)](https://github.com/AnthonyMogotlane/greeting-app-csharp/actions/workflows/unit-test.yml)

Deploying the dotnet core application to a Digital Ocean VPS.

## VPS deployment steps

* Created an ubuntu `server` or `droplet` on Digital Ocean.
 * Naming the server to be called "[firstname]-Server" in Digital Ocean.
* Login to the server using ssh.
* Login to the server using root : `ssh root@your.ip.address`
 
 * Setup on server:
    * Got the latest packages using `apt update` on the server.
    * Install `dotnet` on the server 
     
     Commands to use on Ubuntu `20.04:
     ```
        wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
        sudo dpkg -i packages-microsoft-prod.deb
        rm packages-microsoft-prod.deb
     ```
        
     Followed by:
        
     ```
        sudo apt-get update
        sudo apt-get install -y dotnet-sdk-6.0
     ```
    
    * dotnet installation guidance on Ubuntu [here](https://learn.microsoft.com/en-us/dotnet/core/install/linux-ubuntu)
    * Check if git is installed using `git --version` and if not installed use `apt git `.
    
    * after running these commands the `dotnet` command should work on your server.

* Install `nginx` on the server:
   * [nginx](https://www.nginx.com/) is a web server that we will use as a [reverse proxy](https://docs.nginx.com/nginx/admin-guide/web-server/reverse-proxy/) to send HTTP request to our `dotnet` app
   * Install it using this command: `sudo apt-get install nginx`
   * Start it using this command: `sudo /etc/init.d/nginx start` 
   * You should be able to access the running nginx web server now using this command: `http://your ip adress here`
* Configure `nginx` to forward requests to our `dotnet` web app.
   * Open the `/etc/nginx/sites-available/default` file using the `nano` editor like this `nano /etc/nginx/sites-available/default`
   * and change the `location` section from something like this:

   ```
   location / {
                # First attempt to serve request as file, then
                # as directory, then fall back to displaying a 404.
                try_files $uri $uri/ =404;
   }
   ```
  
   to look like this:

   ```
   location / {
     # First attempt to serve request as file, then
     # as directory, then fall back to displaying a 404.
     # try_files $uri $uri/ =404;
     proxy_pass http://localhost:6007;
     proxy_http_version 1.1;
     proxy_set_header Upgrade $http_upgrade;
     proxy_set_header Connection keep-alive;
     proxy_set_header Host $host;
     proxy_cache_bypass $http_upgrade;
     proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
     proxy_set_header X-Forwarded-Proto $scheme;
   }
   ```
   * Save the changes and restart `nginx` using this command: `sudo /etc/init.d/nginx reload`
   * Check what you see in the browser at: `http://your ip adress here` - you should see an `502 Bad Gateway` error now.
   * This means that it can't find our dotnet app running on port 6007. We will fix that in the next step below.
   
* Run a `dotnet` Web App on the server
    * create an `apps` folder using `mkdir apps` - in your home folder `/root`
    * change into the folder using `cd apps`
    * clone the java project to the server:
        `git clone https://github.com/codex-academy/GreetingsWithRazorPages`
    * change into this folder using`cd GreetingsWithRazorPages/`
    * run these dotnet commands - to restore all local dependencies & to build & run the app:
        
        * `dotnet restore`
        * `dotnet build  -c Release`
        * `dotnet bin/Release/net6.0/GreetingsWithRazorPages.dll --urls=http://localhost:6007/`
        
    * At this point the app should be running at: `http://your-server-ip-address`
    * See if others are able to access your application
    * Please take some screenshots of :
            * your deployed application running the browser
            * your terminal window where you are running the application from
  * stop the process running in the terminal using the ctrl-c command - you should not be able to access your application now.
  
  * run your app in the background using this command:
    ```
    nohup dotnet bin/Release/net6.0/GreetingsWithRazorPages.dll --urls=http://localhost:6007/ > vps.log 2>&1 &
    ```
    
    You can also see this command to the `process id` for the app:
    
    ```
    ps -eaf | grep GreetingsWithRazorPages
    ```
    
## Link sub domains to apps

To link a sub domains to running apps you can add multiple `server` sections in your `/etc/nginx/sites-available/default` file.

Like this:

```
server {

   server_name plusminus.anthony.projectcodex.net;

   location / {
        proxy_pass http://localhost:6007;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;
          
    }

}

server {

   server_name plusminus.anthony.projectcodex.net;

   location / {
        proxy_pass http://localhost:4007;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection 'upgrade';
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
        proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto $scheme;     
    }
}

```

This will allow you to link the sub domain `plusminus.anthony.projectcodex.net` to an app running on port `6007`.
And it will link the sub domain `plusminus.anthony.projectcodex.net` to an app running on port `4007`.

You can add as many `server` sections as you need.

You can read more [here](https://www.digitalocean.com/community/tutorials/how-to-set-up-nginx-server-blocks-virtual-hosts-on-ubuntu-16-04) and [here](https://www.linode.com/docs/guides/how-to-configure-nginx/) or you can just Google `nginx virtual host setup` or something similar. 

 
