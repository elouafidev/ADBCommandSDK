# ADB Command SDK (C-Sharp)

## Description
La bibliothèque ADB Command SDK (C-Sharp) est un ensemble d'outils conçus pour faciliter l'interaction avec les téléphones Android depuis des projets Visual Studio en C-Sharp. Cette bibliothèque utilise des commandes ADB (Android Debug Bridge) pour fournir des fonctionnalités telles que l'accès aux informations du téléphone, la gestion des fichiers, l'installation et la désinstallation d'applications, et bien plus encore.

## Fonctionnalités
- Accès aux informations du téléphone (numéro de série, IMEI, etc.)
- Gestion des fichiers sur le téléphone (copie, suppression, modification)
- Installation et désinstallation d'applications
- Commandes de diagnostic pour la batterie, le stockage, les performances, etc.

## Utilisation
Pour utiliser la bibliothèque ADB Command SDK dans votre projet Visual Studio en C-Sharp, suivez ces étapes :

1. Téléchargez la bibliothèque ADB Command SDK depuis le dépôt GitHub.
2. Ajoutez la bibliothèque à votre projet Visual Studio.
3. Importez les classes nécessaires dans votre code C-Sharp.
4. Appelez les méthodes appropriées pour interagir avec les téléphones Android.

Exemple d'utilisation :

```csharp
using ADBCommandSDK;

class Program
{
    static void Main(string[] args)
    {
        ADBCommand ADBCommand = new ADBCommand();
		
        adbCommand.RunArgument("shell getprop");
	
	\\ Install internal Memory
        adbCommand.InstallAPK("/path/to/app.apk", StateInstallApk.R);
		
	\\Install SD Card
        adbCommand.InstallAPK("/path/to/app.apk", StateInstallApk.R, StateInstallApk.S);
	
	"\nBuild Number :"+ adbCommand.GetBuildNumber();
	"\nDevice product :"+ adbCommand.GetDeviceName();
	"\nBrand :"+ adbCommand.GetBrand();
	"\nBoard Name :"+ adbCommand.GetBoard();
	"\nAndroid Version :"+ adbCommand.GetAndroidVersion();
	"\nCPU :"+dv.GetCPU();

    }
}
```

## Exemple d'Intégration
Pour voir un exemple d'intégration de la bibliothèque ADB Command SDK dans un projet Visual Studio en C-Sharp, consultez le projet  [https://github.com/elouafidev/MobileTools](https://github.com/elouafidev/MobileTools).

## Contact
Pour toute question ou suggestion concernant la bibliothèque ADB Command SDK (C-Sharp), vous pouvez me contacter par email : [support@elouafi.dev](support@elouafi.dev)

## Licence
Ce projet est sous licence MIT. Voir le fichier LICENSE pour plus de détails.

---

Assurez-vous d'adapter ce README en fonction des spécificités de votre bibliothèque SDK C-Sharp. Une fois que vous êtes satisfait du contenu, vous pouvez l'ajouter à votre dépôt GitHub pour que les utilisateurs puissent le consulter.
