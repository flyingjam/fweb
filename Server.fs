open System
open Suave                 // always open suave
open Suave.Http.Successful // for OK-result
open Suave.Http.Applicatives
open Suave.Http.Files
open Suave.Http.Writers
open Suave.Http
open Suave.Sockets
open Suave.Types
open Suave.Web             // for config

let ip = Environment.GetEnvironmentVariable("OPENSHIFT_MONO_IP")
let port = Environment.GetEnvironmentVariable("OPENSHIFT_MONO_PORT")
let root_dir = Environment.GetEnvironmentVariable("OPENSHIFT_REPO_DIR")

//let ip = "127.0.0.1"
//let port = "8080"
let mimeTypeMap = 
    defaultMimeTypesMap
        >=> (function
            | ".mp3" -> mkMimeType "audio/mpeg3" false | _ -> None)

let serverConfig =
    let port = int port
    { defaultConfig with
        bindings = [ Types.HttpBinding.mk' Types.HTTP ip port ]
        mimeTypesMap = mimeTypeMap
    }

let app =
  choose
    [ GET >>= choose
        [ 
          path "/hello" >>= OK "Hello GET"
          path "/" >>= OK "test" 
          path "/image" >>= file (root_dir + "cover.jpg")
        ]
    ]

startWebServer serverConfig app
