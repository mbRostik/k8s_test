#!/bin/sh
set -e

echo ">> Copying CA certificate..."
cp /certs/deployment/ca/K8S.Test.Root.CA.pfx /usr/local/share/ca-certificates/K8S.Test.Root.CA.pfx || true
cp /certs/deployment/ca/K8S.Test.Root.CA.cer /usr/local/share/ca-certificates/K8S.Test.Root.CA.crt || true

echo ">> Updating system certificates..."
update-ca-certificates || true

echo ">> Certificates installed successfully."